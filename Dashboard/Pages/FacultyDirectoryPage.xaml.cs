using System.Collections.ObjectModel;
using Dashboard.Models;
using Dashboard.Services;

namespace Dashboard.Pages;

public partial class FacultyDirectoryPage : ContentPage
{
    private readonly ResearchImageService _researchImageService;
    
    public ObservableCollection<ResearchImage> ResearchImages { get; set; } = new();

    public FacultyDirectoryPage()
    {
        InitializeComponent();
        _researchImageService = Application.Current!.Handler!.MauiContext!.Services.GetService<ResearchImageService>()!;
        BindingContext = this;
        
        // Set all professor image sources from centralized configuration
        SetProfessorImages();
    }

    private void SetProfessorImages()
    {
        // Computer Science Faculty
        LavergneImage.Source = FacultyImageConfig.LavergneImageUrl;
        MenonImage.Source = FacultyImageConfig.MenonImageUrl;
        AndersonImage.Source = FacultyImageConfig.AndersonImageUrl;
        XieImage.Source = FacultyImageConfig.XieImageUrl;
        
        // Engineering Faculty (Alphabetically by last name: Aghili, Ambatipani, Dermisis, Garner, Guo, Li, Liu, Rosti, Subramaniam, Zhang)
        EngProf1Image.Source = FacultyImageConfig.AghiliImageUrl; // Dr. Matthew Aghili
        EngProf2Image.Source = FacultyImageConfig.AmbatipaniImageUrl; // Dr. Srinivasam Ambatipani
        EngProf3Image.Source = FacultyImageConfig.DermisisImageUrl; // Dr. Dimitrios Dermisis
        EngProf4Image.Source = FacultyImageConfig.GarnerImageUrl; // Mr. Brent Garner
        EngProf5Image.Source = FacultyImageConfig.GuoImageUrl; // Dr. Qi Guo
        EngProf6Image.Source = FacultyImageConfig.LiImageUrl; // Dr. Zhuang Li
        EngProf7Image.Source = FacultyImageConfig.LiuImageUrl; // Dr. Qiu Liu
        EngProf8Image.Source = FacultyImageConfig.RostiImageUrl; // Dr. Firouz Rosti
        EngProf9Image.Source = FacultyImageConfig.SubramaniamImageUrl; // Dr. Ramalingam Subramaniam
        EngProf10Image.Source = FacultyImageConfig.ZhangImageUrl; // Dr. Ning Zhang
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadResearchImages();
    }

    private async Task LoadResearchImages()
    {
        try
        {
            var connectionOk = await _researchImageService.TestConnectionAsync();
            System.Diagnostics.Debug.WriteLine($"Faculty research images connection test: {connectionOk}");

            var researchImages = await _researchImageService.GetFacultyResearchImagesAsync();
            System.Diagnostics.Debug.WriteLine($"Loaded {researchImages.Count} faculty research images");

            ResearchImages.Clear();
            foreach (var image in researchImages)
            {
                ResearchImages.Add(image);
            }

            // Add fallback data if no images found
            if (ResearchImages.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No faculty research images found, adding fallback data");
                ResearchImages.Add(new ResearchImage
                {
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty_research/image%20(2).png",
                    Caption = "VR Lab Setup",
                    FileName = "vr_lab_setup.jpg"
                });
                
                ResearchImages.Add(new ResearchImage
                {
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty_research/image.png",
                    Caption = "Game Development",
                    FileName = "game_development.jpg"
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading faculty research images: {ex.Message}");
        }
    }

    private void ShowResearchModal(string professorName)
    {
        try
        {
            ModalProfessorName.Text = professorName;
            ModalResearchContent.Children.Clear();
            
            // Populate content based on professor
            if (professorName.Contains("Lavergne"))
            {
                PopulateLavergneResearchContent();
            }
            else if (professorName.Contains("Menon"))
            {
                PopulatePlaceholderResearchContent("Dr. Vipin Menon");
            }
            else if (professorName.Contains("Anderson"))
            {
                PopulatePlaceholderResearchContent("Ms. Rhonda Anderson");
            }
            else if (professorName.Contains("Xie"))
            {
                PopulatePlaceholderResearchContent("Dr. Bei Xie");
            }
            else
            {
                PopulatePlaceholderResearchContent(professorName);
            }
            
            ResearchModalOverlay.IsVisible = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error showing research modal: {ex.Message}");
        }
    }

    private void PopulateLavergneResearchContent()
    {
        // Research Interests
        var researchFocusFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var researchFocusStack = new VerticalStackLayout { Spacing = 10 };
        researchFocusStack.Children.Add(new Label
        {
            Text = "Research Focus",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        researchFocusStack.Children.Add(new Label { Text = "• Video Game Development & Mathematics", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Virtual Reality (VR) Technology", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Data Visualization & High-Dimensional Datasets", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Procedural Generation Algorithms", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Interactive Digital Environments", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusFrame.Content = researchFocusStack;
        ModalResearchContent.Children.Add(researchFocusFrame);

        // Current Research Projects
        var projectsFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var projectsStack = new VerticalStackLayout { Spacing = 10 };
        projectsStack.Children.Add(new Label
        {
            Text = "Current Research Projects",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        // Project 1
        var project1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var project1Stack = new VerticalStackLayout { Spacing = 5 };
        project1Stack.Children.Add(new Label { Text = "Virtual Reality Lab Launch", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        project1Stack.Children.Add(new Label { Text = "Currently preparing to launch a new Virtual Reality lab at McNeese State University, aimed at expanding student engagement and advancing meaningful, creative research in immersive digital environments.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        project1Stack.Children.Add(new Label { Text = "Institution: McNeese State University | Status: In Development", FontSize = 10, TextColor = Color.FromArgb("#f2b32e"), FontAttributes = FontAttributes.Italic });
        project1Frame.Content = project1Stack;
        projectsStack.Children.Add(project1Frame);
        
        // Project 2
        var project2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var project2Stack = new VerticalStackLayout { Spacing = 5 };
        project2Stack.Children.Add(new Label { Text = "Procedural Generation in 2D Games", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        project2Stack.Children.Add(new Label { Text = "Exploring the use of mathematics and programming to procedurally generate randomized levels in 2D video games. Students develop dungeon crawler games using Unity and Godot engines to compare Brute Force and Binary Space Partitioning (BSP) algorithms.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        project2Stack.Children.Add(new Label { Text = "Engines: Unity & Godot | Focus: Undergraduate Research", FontSize = 10, TextColor = Color.FromArgb("#f2b32e"), FontAttributes = FontAttributes.Italic });
        project2Frame.Content = project2Stack;
        projectsStack.Children.Add(project2Frame);
        
        // Project 3
        var project3Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var project3Stack = new VerticalStackLayout { Spacing = 5 };
        project3Stack.Children.Add(new Label { Text = "Data Visualization & User Experience", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        project3Stack.Children.Add(new Label { Text = "Bridging video game development, mathematics, and visualization of actionable insights from large, high-dimensional datasets. Focus on enhancing user experience and improving computational efficiency in interactive digital environments.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        project3Stack.Children.Add(new Label { Text = "Focus: Interdisciplinary Collaboration | Impact: Industry-Relevant", FontSize = 10, TextColor = Color.FromArgb("#f2b32e"), FontAttributes = FontAttributes.Italic });
        project3Frame.Content = project3Stack;
        projectsStack.Children.Add(project3Frame);
        
        projectsFrame.Content = projectsStack;
        ModalResearchContent.Children.Add(projectsFrame);

        // Research Impact
        var impactFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var impactStack = new VerticalStackLayout { Spacing = 10 };
        impactStack.Children.Add(new Label
        {
            Text = "Research Impact & Student Development",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var impact1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var impact1Stack = new VerticalStackLayout { Spacing = 5 };
        impact1Stack.Children.Add(new Label { Text = "Undergraduate Research Leadership", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White });
        impact1Stack.Children.Add(new Label { Text = "Leads an undergraduate research group emphasizing hands-on experience with industry-relevant projects, helping students build strong, competitive résumés for the fast-paced software engineering workforce.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        impact1Stack.Children.Add(new Label { Text = "Focus: Real-world Problem Solving & Innovation", FontSize = 10, TextColor = Color.FromArgb("#FFD204") });
        impact1Frame.Content = impact1Stack;
        impactStack.Children.Add(impact1Frame);
        
        var impact2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var impact2Stack = new VerticalStackLayout { Spacing = 5 };
        impact2Stack.Children.Add(new Label { Text = "Algorithm Comparison Research", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White });
        impact2Stack.Children.Add(new Label { Text = "Brute Force method produces more natural, human-like level designs enhancing immersion and player comfort, while BSP method creates structured, mathematically precise environments with greater computational efficiency.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        impact2Stack.Children.Add(new Label { Text = "Contribution: Game Development & Computational Mathematics", FontSize = 10, TextColor = Color.FromArgb("#00aeef") });
        impact2Frame.Content = impact2Stack;
        impactStack.Children.Add(impact2Frame);
        
        impactFrame.Content = impactStack;
        ModalResearchContent.Children.Add(impactFrame);

        // Research Gallery
        var galleryFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var galleryStack = new VerticalStackLayout { Spacing = 15 };
        galleryStack.Children.Add(new Label
        {
            Text = "Research Gallery",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        galleryStack.Children.Add(new Label
        {
            Text = "Visual highlights from Dr. Lavergne's game development and VR research",
            FontSize = 12,
            TextColor = Color.FromArgb("#CCCCCC")
        });
        
        var collectionView = new CollectionView
        {
            ItemsSource = ResearchImages,
            ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical),
            HeightRequest = 200
        };
        collectionView.ItemTemplate = new DataTemplate(() =>
        {
            var itemFrame = new Frame
            {
                BackgroundColor = Color.FromArgb("#002a54"),
                CornerRadius = 8,
                Padding = 10,
                HasShadow = false
            };
            var itemStack = new VerticalStackLayout { Spacing = 8, HorizontalOptions = LayoutOptions.Center };
            var image = new Image { Aspect = Aspect.AspectFill, HeightRequest = 80 };
            image.SetBinding(Image.SourceProperty, "ImageUrl");
            var caption = new Label { FontSize = 10, TextColor = Color.FromArgb("#CCCCCC"), HorizontalOptions = LayoutOptions.Center };
            caption.SetBinding(Label.TextProperty, "Caption");
            itemStack.Children.Add(image);
            itemStack.Children.Add(caption);
            itemFrame.Content = itemStack;
            return itemFrame;
        });
        galleryStack.Children.Add(collectionView);
        galleryFrame.Content = galleryStack;
        ModalResearchContent.Children.Add(galleryFrame);

        // Contact Information
        var contactFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 20,
            HasShadow = false
        };
        var contactStack = new VerticalStackLayout { Spacing = 15 };
        contactStack.Children.Add(new Label
        {
            Text = "Contact Information",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center
        });
        
        var contactDetailsStack = new VerticalStackLayout { Spacing = 12 };
        
        var officeStack = new HorizontalStackLayout { Spacing = 8, HorizontalOptions = LayoutOptions.Center };
        var officeLabelStack = new VerticalStackLayout { Spacing = 3 };
        officeLabelStack.Children.Add(new Label { Text = "Office Location", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204"), HorizontalOptions = LayoutOptions.Center });
        officeLabelStack.Children.Add(new Label { Text = "Drew Hall, Room 201", FontSize = 16, TextColor = Colors.White, HorizontalOptions = LayoutOptions.Center });
        officeStack.Children.Add(officeLabelStack);
        contactDetailsStack.Children.Add(officeStack);
        
        var phoneStack = new HorizontalStackLayout { Spacing = 8, HorizontalOptions = LayoutOptions.Center };
        phoneStack.Children.Add(new Label { Text = "📞", FontSize = 18, VerticalOptions = LayoutOptions.Center });
        var phoneLabelStack = new VerticalStackLayout { Spacing = 3 };
        phoneLabelStack.Children.Add(new Label { Text = "Phone", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef"), HorizontalOptions = LayoutOptions.Center });
        phoneLabelStack.Children.Add(new Label { Text = "(337) 475-5852", FontSize = 16, TextColor = Colors.White, HorizontalOptions = LayoutOptions.Center });
        phoneStack.Children.Add(phoneLabelStack);
        contactDetailsStack.Children.Add(phoneStack);
        
        var hoursStack = new HorizontalStackLayout { Spacing = 8, HorizontalOptions = LayoutOptions.Center };
        var hoursLabelStack = new VerticalStackLayout { Spacing = 3 };
        hoursLabelStack.Children.Add(new Label { Text = "Office Hours", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#f2b32e"), HorizontalOptions = LayoutOptions.Center });
        hoursLabelStack.Children.Add(new Label { Text = "Mon, Wed, Fri: 10:00 AM - 12:00 PM", FontSize = 16, TextColor = Colors.White, HorizontalOptions = LayoutOptions.Center });
        hoursStack.Children.Add(hoursLabelStack);
        contactDetailsStack.Children.Add(hoursStack);
        
        contactStack.Children.Add(contactDetailsStack);
        contactFrame.Content = contactStack;
        ModalResearchContent.Children.Add(contactFrame);
    }

    private void PopulatePlaceholderResearchContent(string professorName)
    {
        var researchFocusFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var researchFocusStack = new VerticalStackLayout { Spacing = 10 };
        researchFocusStack.Children.Add(new Label
        {
            Text = "Research Focus",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        researchFocusStack.Children.Add(new Label { Text = "• Research Area 1", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Research Area 2", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Research Area 3", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusFrame.Content = researchFocusStack;
        ModalResearchContent.Children.Add(researchFocusFrame);
    }

    private void OnCloseModalClicked(object sender, EventArgs e)
    {
        ResearchModalOverlay.IsVisible = false;
    }

    private void OnModalBackgroundTapped(object sender, EventArgs e)
    {
        ResearchModalOverlay.IsVisible = false;
    }

    // Computer Science Professors
    private void OnLavergneExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Jennifer Lavergne");
    }

    private void OnMenonExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Vipin Menon");
    }

    private void OnAndersonExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Ms. Rhonda Anderson");
    }

    private void OnXieExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Bei Xie");
    }

    // Engineering Professors (Alphabetically by last name)
    private void OnEngProf1ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Matthew Aghili");
    }

    private void OnEngProf2ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Srinivasam Ambatipani");
    }

    private void OnEngProf3ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Dimitrios Dermisis");
    }

    private void OnEngProf4ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Mr. Brent Garner");
    }

    private void OnEngProf5ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Qi Guo");
    }

    private void OnEngProf6ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Zhuang Li");
    }

    private void OnEngProf7ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Qiu Liu");
    }

    private void OnEngProf8ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Firouz Rosti");
    }

    private void OnEngProf9ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Ramalingam Subramaniam");
    }

    private void OnEngProf10ExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Ning Zhang");
    }
}
