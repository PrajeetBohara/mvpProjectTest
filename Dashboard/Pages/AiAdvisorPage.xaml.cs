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
    private const int RefreshIntervalMs = 2000; // Poll every 2 seconds

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
        _ = _vm.StartAutoRefreshAsync(_pollCts.Token, RefreshIntervalMs);
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

    public async Task StartAutoRefreshAsync(CancellationToken token, int intervalMs)
    {
        while (!token.IsCancellationRequested)
        {
            await RefreshAsync();
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
            Messages.Clear();
            foreach (var m in transcript)
            {
                Messages.Add(m);
            }
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

