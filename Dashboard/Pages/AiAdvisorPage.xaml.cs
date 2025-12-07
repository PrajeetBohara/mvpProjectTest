using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

public partial class AiAdvisorPage : ContentPage
{
    private readonly AiAdvisorMirrorService _mirrorService;
    private readonly AiAdvisorViewModel _vm;
    private CancellationTokenSource? _pollCts;
    private const int CheckIntervalMs = 1000; // Check for updates every 1 second
    private string? _lastKnownUpdateTime;

    public AiAdvisorPage()
    {
        InitializeComponent();
        _mirrorService = new AiAdvisorMirrorService();
        _vm = new AiAdvisorViewModel(_mirrorService);
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _pollCts?.Cancel();
        _pollCts = new CancellationTokenSource();
        _lastKnownUpdateTime = null;
        _ = _vm.StartSmartRefreshAsync(_mirrorService, _pollCts.Token, CheckIntervalMs, (time) => _lastKnownUpdateTime = time);
        await _vm.RefreshAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _pollCts?.Cancel();
        _pollCts = null;
        _ = _vm.ClearSessionAsync();
    }
}

public class AiAdvisorViewModel : BindableObject
{
    private readonly AiAdvisorMirrorService _mirrorService;
    private bool _isLoading;

    public ObservableCollection<AiAdvisorMessage> Messages { get; } = new();

    public string ChatUrl => AiAdvisorConfig.ChatUrl;

    public string QrImageUrl => BuildQrUrl(AiAdvisorConfig.ChatUrl);

    public ICommand RefreshCommand { get; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading == value) return;
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public AiAdvisorViewModel(AiAdvisorMirrorService mirrorService)
    {
        _mirrorService = mirrorService;
        RefreshCommand = new Command(async () => await RefreshAsync());
    }

    public async Task StartSmartRefreshAsync(AiAdvisorMirrorService mirrorService, CancellationToken token, int intervalMs, Action<string?> updateLastKnownTime)
    {
        string? lastKnownTime = null;
        
        while (!token.IsCancellationRequested)
        {
            try
            {
                // Check if there's a new update
                var currentLastUpdated = await mirrorService.GetLastUpdatedAsync(token);
                
                // Only refresh if the timestamp changed
                if (currentLastUpdated != null && currentLastUpdated != lastKnownTime)
                {
                    System.Diagnostics.Debug.WriteLine($"[ViewModel] Detected update: {lastKnownTime} -> {currentLastUpdated}");
                    lastKnownTime = currentLastUpdated;
                    updateLastKnownTime(currentLastUpdated);
                    await RefreshAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ViewModel] Error in smart refresh: {ex.Message}");
            }
            
            try
            {
                await Task.Delay(intervalMs, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    public async Task RefreshAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var transcript = await _mirrorService.GetTranscriptAsync();
            
            System.Diagnostics.Debug.WriteLine($"[ViewModel] Received {transcript.Count} messages from service");
            
            // Update UI on main thread
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Messages.Clear();
                foreach (var m in transcript)
                {
                    System.Diagnostics.Debug.WriteLine($"[ViewModel] Adding message: Role={m.Role}, Content={m.Content?.Substring(0, Math.Min(50, m.Content?.Length ?? 0))}...");
                    Messages.Add(m);
                }
                System.Diagnostics.Debug.WriteLine($"[ViewModel] Total messages in collection: {Messages.Count}");
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ViewModel] Error in RefreshAsync: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[ViewModel] Stack trace: {ex.StackTrace}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task ClearSessionAsync()
    {
        await _mirrorService.ClearTranscriptAsync();
        Messages.Clear();
    }

    private static string BuildQrUrl(string target)
    {
        if (string.IsNullOrWhiteSpace(target))
            return "https://api.qrserver.com/v1/create-qr-code/?size=240x240&data=about:blank";

        var encoded = Uri.EscapeDataString(target);
        return $"https://api.qrserver.com/v1/create-qr-code/?size=240x240&data={encoded}";
    }
}

