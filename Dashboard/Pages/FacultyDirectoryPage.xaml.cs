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
        // Admin Section
        AmbatipatiImage.Source = FacultyImageConfig.AmbatipaniImageUrl; // Dr. Vasan Ambatipati (Department Head)
        ZhangAdminImage.Source = FacultyImageConfig.ZhangImageUrl; // Dr. Ning Zhang
        OBrienImage.Source = FacultyImageConfig.OBrienImageUrl; // Mrs. Ramona O'Brien
        BennettImage.Source = FacultyImageConfig.BennettImageUrl; // Mr. Dennis Bennett
        
        // Coordinators Section
        AghiliImage.Source = FacultyImageConfig.AghiliImageUrl; // Dr. Matthew Aghili
        DermisisImage.Source = FacultyImageConfig.DermisisImageUrl; // Dr. Dimitrios Dermisis
        GarnerImage.Source = FacultyImageConfig.GarnerImageUrl; // Mr. Brent Garner
        LiImage.Source = FacultyImageConfig.LiImageUrl; // Dr. Zhuang Li
        MenonCoordinatorImage.Source = FacultyImageConfig.MenonImageUrl; // Dr. Vipin Menon
        SubramaniamImage.Source = FacultyImageConfig.SubramaniamImageUrl; // Dr. Ramalingam Subramaniam
        
        // Faculty Section (sorted by last name: Ambatipati, Anderson, Guo, Lavergne, Liu, Menon, Rosti, Xie, Zhang, Zeitoun)
        AmbatipatiFacultyImage.Source = FacultyImageConfig.AmbatipaniImageUrl; // Dr. Vasan Ambatipati
        AndersonImage.Source = FacultyImageConfig.AndersonImageUrl; // Ms. Rhonda Anderson
        GuoImage.Source = FacultyImageConfig.GuoImageUrl; // Dr. Qi Guo
        LavergneFacultyImage.Source = FacultyImageConfig.LavergneImageUrl; // Dr. Jennifer Lavergne
        LiuImage.Source = FacultyImageConfig.LiuImageUrl; // Dr. Qiu Liu
        MenonFacultyImage.Source = FacultyImageConfig.MenonImageUrl; // Dr. Vipin Menon
        RostiImage.Source = FacultyImageConfig.RostiImageUrl; // Dr. Firouz Rosti
        XieImage.Source = FacultyImageConfig.XieImageUrl; // Dr. Bei Xie
        ZhangImage.Source = FacultyImageConfig.ZhangImageUrl; // Dr. Ning Zhang
        ZeitounImage.Source = FacultyImageConfig.ZeitounImageUrl; // Dr. Zeyad Zeitoun
        SinghImage.Source = FacultyImageConfig.SinghImageUrl; // Prithvi Raj Singh
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
            
            // Set professor image using the same URL as the faculty card
            SetModalProfessorImage(professorName);
            
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
                PopulateAndersonResearchContent();
            }
            else if (professorName.Contains("Xie"))
            {
                PopulateXieResearchContent();
            }
            else if (professorName.Contains("Zhang"))
            {
                PopulateZhangResearchContent();
            }
            else if (professorName.Contains("Guo"))
            {
                PopulateGuoResearchContent();
            }
            else if (professorName.Contains("Rosti"))
            {
                PopulateRostiResearchContent();
            }
            else if (professorName.Contains("Zeitoun"))
            {
                PopulateZeitounResearchContent();
            }
            else if ((professorName.Contains("Ambatipati") || professorName.Contains("Ambatipani")) && !professorName.Contains("Zeitoun"))
            {
                PopulateAmbatipatiResearchContent();
            }
            else if (professorName.Contains("Garner"))
            {
                PopulateGarnerResearchContent();
            }
            else if (professorName.Contains("Singh"))
            {
                PopulateSinghResearchContent();
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

    private void SetModalProfessorImage(string professorName)
    {
        try
        {
            // Set the modal image using the same URL as the faculty card
            if (professorName.Contains("Lavergne"))
                ModalProfessorImage.Source = FacultyImageConfig.LavergneImageUrl;
            else if (professorName.Contains("Menon"))
                ModalProfessorImage.Source = FacultyImageConfig.MenonImageUrl;
            else if (professorName.Contains("Anderson"))
                ModalProfessorImage.Source = FacultyImageConfig.AndersonImageUrl;
            else if (professorName.Contains("Xie"))
                ModalProfessorImage.Source = FacultyImageConfig.XieImageUrl;
            else if (professorName.Contains("Zeitoun"))
                ModalProfessorImage.Source = FacultyImageConfig.ZeitounImageUrl;
            else if ((professorName.Contains("Ambatipati") || professorName.Contains("Ambatipani")) && !professorName.Contains("Zeitoun"))
                ModalProfessorImage.Source = FacultyImageConfig.AmbatipaniImageUrl;
            else if (professorName.Contains("Garner"))
                ModalProfessorImage.Source = FacultyImageConfig.GarnerImageUrl;
            else if (professorName.Contains("Singh"))
                ModalProfessorImage.Source = FacultyImageConfig.SinghImageUrl;
            else if (professorName.Contains("Aghili"))
                ModalProfessorImage.Source = FacultyImageConfig.AghiliImageUrl;
            else if (professorName.Contains("Dermisis"))
                ModalProfessorImage.Source = FacultyImageConfig.DermisisImageUrl;
            else if (professorName.Contains("Li") && !professorName.Contains("Liu"))
                ModalProfessorImage.Source = FacultyImageConfig.LiImageUrl;
            else if (professorName.Contains("Subramaniam"))
                ModalProfessorImage.Source = FacultyImageConfig.SubramaniamImageUrl;
            else if (professorName.Contains("Guo"))
                ModalProfessorImage.Source = FacultyImageConfig.GuoImageUrl;
            else if (professorName.Contains("Liu"))
                ModalProfessorImage.Source = FacultyImageConfig.LiuImageUrl;
            else if (professorName.Contains("Rosti"))
                ModalProfessorImage.Source = FacultyImageConfig.RostiImageUrl;
            else if (professorName.Contains("Zhang"))
                ModalProfessorImage.Source = FacultyImageConfig.ZhangImageUrl;
            else
                ModalProfessorImage.Source = null; // Default fallback
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error setting modal professor image: {ex.Message}");
            ModalProfessorImage.Source = null;
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

    }

    private void PopulatePlaceholderResearchContent(string professorName)
    {
        var announcementFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 20,
            HasShadow = false
        };
        var announcementStack = new VerticalStackLayout { Spacing = 10 };
        announcementStack.Children.Add(new Label
        {
            Text = "Research Information",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center
        });
        announcementStack.Children.Add(new Label 
        { 
            Text = "To be announced", 
            FontSize = 16, 
            TextColor = Color.FromArgb("#FFD204"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Italic
        });
        announcementStack.Children.Add(new Label 
        { 
            Text = "Research details for this faculty member will be available soon.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.WordWrap
        });
        announcementFrame.Content = announcementStack;
        ModalResearchContent.Children.Add(announcementFrame);
    }

    private void PopulateZeitounResearchContent()
    {
        // Research Focus
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
        researchFocusStack.Children.Add(new Label { Text = "• Wastewater Treatment Technologies", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Multiphase Reactor Hydrodynamics", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Fourth-Generation Nuclear Reactor Design", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Carbon Capture and Mineralization", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusFrame.Content = researchFocusStack;
        ModalResearchContent.Children.Add(researchFocusFrame);

        // Education
        var educationFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var educationStack = new VerticalStackLayout { Spacing = 10 };
        educationStack.Children.Add(new Label
        {
            Text = "Education",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var phdFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var phdStack = new VerticalStackLayout { Spacing = 5 };
        phdStack.Children.Add(new Label { Text = "Ph.D. in Chemical Engineering", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        phdStack.Children.Add(new Label { Text = "Missouri University of Science and Technology", FontSize = 14, TextColor = Colors.White });
        phdStack.Children.Add(new Label { Text = "GPA: 3.7/4.0 | Advisor: Prof. Dr. Muthanna Al-Dahhan", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        phdFrame.Content = phdStack;
        educationStack.Children.Add(phdFrame);
        
        var mscFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var mscStack = new VerticalStackLayout { Spacing = 5 };
        mscStack.Children.Add(new Label { Text = "M.Sc. in Chemical Engineering", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        mscStack.Children.Add(new Label { Text = "Alexandria University, Egypt", FontSize = 14, TextColor = Colors.White });
        mscStack.Children.Add(new Label { Text = "GPA: 3.7/4.0", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        mscFrame.Content = mscStack;
        educationStack.Children.Add(mscFrame);
        
        var bscFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var bscStack = new VerticalStackLayout { Spacing = 5 };
        bscStack.Children.Add(new Label { Text = "B.Sc. in Chemical Engineering", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        bscStack.Children.Add(new Label { Text = "Alexandria University, Egypt", FontSize = 14, TextColor = Colors.White });
        bscStack.Children.Add(new Label { Text = "GPA: 3.9/4.0 | Ranked 1st out of 148 graduates", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        bscFrame.Content = bscStack;
        educationStack.Children.Add(bscFrame);
        
        educationFrame.Content = educationStack;
        ModalResearchContent.Children.Add(educationFrame);

        // Current Position & Research
        var currentPositionFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var currentPositionStack = new VerticalStackLayout { Spacing = 10 };
        currentPositionStack.Children.Add(new Label
        {
            Text = "Current Position & Research",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var positionFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var positionStack = new VerticalStackLayout { Spacing = 5 };
        positionStack.Children.Add(new Label { Text = "Tenure-Track Assistant Professor", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        positionStack.Children.Add(new Label { Text = "McNeese State University | Chemical Engineering Department", FontSize = 14, TextColor = Colors.White });
        positionStack.Children.Add(new Label { Text = "Teaching: Reactor Engineering, Process Safety, Unit Operations, Mass Transfer Operations", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        positionStack.Children.Add(new Label { Text = "Research: Multiphase reactors, advanced wastewater treatment, carbon capture and utilization", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        positionFrame.Content = positionStack;
        currentPositionStack.Children.Add(positionFrame);
        
        currentPositionFrame.Content = currentPositionStack;
        ModalResearchContent.Children.Add(currentPositionFrame);

        // Active Funding & Projects
        var fundingFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var fundingStack = new VerticalStackLayout { Spacing = 10 };
        fundingStack.Children.Add(new Label
        {
            Text = "Active Funding & Projects",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var project1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var project1Stack = new VerticalStackLayout { Spacing = 5 };
        project1Stack.Children.Add(new Label { Text = "Advanced Electrocoagulation for Industrial Wastewater Treatment", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        project1Stack.Children.Add(new Label { Text = "A Sustainable Solution for PFAS Removal and Water Quality Improvement", FontSize = 14, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        project1Stack.Children.Add(new Label { Text = "Louisiana Sea Grant | PI | $10,000 (March 2025 - February 2026)", FontSize = 12, TextColor = Color.FromArgb("#00aeef"), FontAttributes = FontAttributes.Italic });
        project1Frame.Content = project1Stack;
        fundingStack.Children.Add(project1Frame);
        
        var project2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var project2Stack = new VerticalStackLayout { Spacing = 5 };
        project2Stack.Children.Add(new Label { Text = "Converting CO₂ and Alkaline Solid Wastes into Carbon-Negative Materials", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        project2Stack.Children.Add(new Label { Text = "Co-decarbonization of Multiple Sectors", FontSize = 14, TextColor = Colors.White });
        project2Stack.Children.Add(new Label { Text = "DOE-NETL | Postdoc | $2,500,000", FontSize = 12, TextColor = Color.FromArgb("#f2b32e"), FontAttributes = FontAttributes.Italic });
        project2Frame.Content = project2Stack;
        fundingStack.Children.Add(project2Frame);
        
        fundingFrame.Content = fundingStack;
        ModalResearchContent.Children.Add(fundingFrame);

        // Awards & Recognition
        var awardsFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var awardsStack = new VerticalStackLayout { Spacing = 10 };
        awardsStack.Children.Add(new Label
        {
            Text = "Awards & Recognition",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var award1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var award1Stack = new VerticalStackLayout { Spacing = 5 };
        award1Stack.Children.Add(new Label { Text = "Third Place - Best Presentation Award", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        award1Stack.Children.Add(new Label { Text = "AIChE Annual Meeting 2025 | Nuclear Engineering Division", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        award1Frame.Content = award1Stack;
        awardsStack.Children.Add(award1Frame);
        
        var award2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var award2Stack = new VerticalStackLayout { Spacing = 5 };
        award2Stack.Children.Add(new Label { Text = "ASPEN HYSYS Certified User", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        award2Stack.Children.Add(new Label { Text = "AspenTech University | March 2025", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        award2Frame.Content = award2Stack;
        awardsStack.Children.Add(award2Frame);
        
        var award3Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var award3Stack = new VerticalStackLayout { Spacing = 5 };
        award3Stack.Children.Add(new Label { Text = "Recognized Reviewer Certificate", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        award3Stack.Children.Add(new Label { Text = "2023 ACS Publication Peer Reviewer", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        award3Frame.Content = award3Stack;
        awardsStack.Children.Add(award3Frame);
        
        awardsFrame.Content = awardsStack;
        ModalResearchContent.Children.Add(awardsFrame);

        // Key Publications
        var publicationsFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var publicationsStack = new VerticalStackLayout { Spacing = 10 };
        publicationsStack.Children.Add(new Label
        {
            Text = "Selected Publications",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        publicationsStack.Children.Add(new Label
        {
            Text = "12+ peer-reviewed journal articles in leading chemical engineering and nuclear engineering journals",
            FontSize = 12,
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        
        var pub1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var pub1Stack = new VerticalStackLayout { Spacing = 5 };
        pub1Stack.Children.Add(new Label { Text = "Natural Circulation Phenomena in Micro Prismatic Modular Reactor", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        pub1Stack.Children.Add(new Label { Text = "Nuclear Engineering and Design, 2025", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        pub1Frame.Content = pub1Stack;
        publicationsStack.Children.Add(pub1Frame);
        
        var pub2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var pub2Stack = new VerticalStackLayout { Spacing = 5 };
        pub2Stack.Children.Add(new Label { Text = "Characterizing Passive Flow in Nuclear Prismatic Modular Reactor Core Channels", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        pub2Stack.Children.Add(new Label { Text = "Applied Thermal Engineering, 2024", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        pub2Frame.Content = pub2Stack;
        publicationsStack.Children.Add(pub2Frame);
        
        var pub3Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var pub3Stack = new VerticalStackLayout { Spacing = 5 };
        pub3Stack.Children.Add(new Label { Text = "Textile Wastewater Treatment by Coupling TiO₂ with PVDF Membrane", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        pub3Stack.Children.Add(new Label { Text = "Bulletin of the National Research Centre, 2023", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC") });
        pub3Frame.Content = pub3Stack;
        publicationsStack.Children.Add(pub3Frame);
        
        publicationsFrame.Content = publicationsStack;
        ModalResearchContent.Children.Add(publicationsFrame);

    }

    private (string officeLocation, string email) GetFacultyContactInfo(string professorName)
    {
        // Office locations and emails based on the provided images
        if (professorName.Contains("Lavergne"))
            return ("Drew 209", "jlavergne@mcneese.edu");
        if (professorName.Contains("Aghili"))
            return ("Drew 323", "draghili@mcneese.edu");
        if (professorName.Contains("Dermisis"))
            return ("Drew Hall", "ddermisis@mcneese.edu"); // Not in images, using placeholder
        if (professorName.Contains("Garner"))
            return ("Drew Hall 103", "garner@mcneese.edu");
        if (professorName.Contains("Li") && !professorName.Contains("Liu"))
            return ("Drew 104", "zli@mcneese.edu");
        if (professorName.Contains("Subramaniam"))
            return ("Drew 134", "rsubramaniam@mcneese.edu");
        if (professorName.Contains("Ambatipati") || professorName.Contains("Ambatipani"))
            return ("Drew 115", "sambatipani@mcneese.edu");
        if (professorName.Contains("Anderson"))
            return ("Drew 207", "randerson@mcneese.edu");
        if (professorName.Contains("Guo"))
            return ("Drew Hall", "qguo@mcneese.edu");
        if (professorName.Contains("Liu"))
            return ("Drew 313", "qliu@mcneese.edu");
        if (professorName.Contains("Menon"))
            return ("Drew 204", "menon@mcneese.edu");
        if (professorName.Contains("Rosti"))
            return ("Drew Hall 133", "frosti@mcneese.edu");
        if (professorName.Contains("Xie"))
            return ("Drew 120", "bxie@mcneese.edu");
        if (professorName.Contains("Zhang"))
            return ("Drew 119", "nzhang@mcneese.edu");
        if (professorName.Contains("Zeitoun"))
            return ("Drew Hall", "zzeitoun@mcneese.edu");
        if (professorName.Contains("Singh"))
            return ("Drew Hall", "psingh8@mcneese.edu");
        if (professorName.Contains("Garner"))
            return ("Drew Hall 103", "garner@mcneese.edu"); // Updated from images
        
        // Default fallback
        return ("Drew Hall", "");
    }

    private void AddContactInformation(string officeLocation, string email)
    {
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
        
        var contactDetailsStack = new VerticalStackLayout { Spacing = 15 };
        
        // Office Location
        var officeStack = new VerticalStackLayout { Spacing = 5, HorizontalOptions = LayoutOptions.Center };
        officeStack.Children.Add(new Label 
        { 
            Text = "Office Location", 
            FontSize = 14, 
            FontAttributes = FontAttributes.Bold, 
            TextColor = Color.FromArgb("#FFD204"), 
            HorizontalOptions = LayoutOptions.Center 
        });
        officeStack.Children.Add(new Label 
        { 
            Text = officeLocation, 
            FontSize = 16, 
            TextColor = Colors.White, 
            HorizontalOptions = LayoutOptions.Center 
        });
        contactDetailsStack.Children.Add(officeStack);
        
        // Email
        if (!string.IsNullOrEmpty(email))
        {
            var emailStack = new VerticalStackLayout { Spacing = 5, HorizontalOptions = LayoutOptions.Center };
            emailStack.Children.Add(new Label 
            { 
                Text = "Email", 
                FontSize = 14, 
                FontAttributes = FontAttributes.Bold, 
                TextColor = Color.FromArgb("#00aeef"), 
                HorizontalOptions = LayoutOptions.Center 
            });
            emailStack.Children.Add(new Label 
            { 
                Text = email, 
                FontSize = 16, 
                TextColor = Colors.White, 
                HorizontalOptions = LayoutOptions.Center 
            });
            contactDetailsStack.Children.Add(emailStack);
        }
        
        contactStack.Children.Add(contactDetailsStack);
        contactFrame.Content = contactStack;
        ModalResearchContent.Children.Add(contactFrame);
    }

    private void PopulateAmbatipatiResearchContent()
    {
        // Research Focus
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
        researchFocusStack.Children.Add(new Label { Text = "• Natural Gas Conversion and In-Situ Chemical Production", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Sustainable Technologies for Oil & Gas Operations", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Environmental Applications: Sulfide and Paraffin Treatment", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Engineering Education and Curriculum Development", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusFrame.Content = researchFocusStack;
        ModalResearchContent.Children.Add(researchFocusFrame);

        // Research and Innovation
        var innovationFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var innovationStack = new VerticalStackLayout { Spacing = 10 };
        innovationStack.Children.Add(new Label
        {
            Text = "Research and Innovation",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var innov1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var innov1Stack = new VerticalStackLayout { Spacing = 5 };
        innov1Stack.Children.Add(new Label { Text = "Natural Gas Conversion Technologies", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        innov1Stack.Children.Add(new Label { Text = "Specializes in developing technologies for converting natural gas into valuable chemicals and fuels. Research includes in-situ chemical production methods aimed at enhancing efficiency and sustainability in oil and gas operations.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        innov1Frame.Content = innov1Stack;
        innovationStack.Children.Add(innov1Frame);
        
        var innov2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var innov2Stack = new VerticalStackLayout { Spacing = 5 };
        innov2Stack.Children.Add(new Label { Text = "Environmental Solutions", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        innov2Stack.Children.Add(new Label { Text = "Innovative solutions for treating sulfides and paraffin in oil field waters, contributing to more environmentally friendly practices in the industry.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        innov2Frame.Content = innov2Stack;
        innovationStack.Children.Add(innov2Frame);
        
        innovationFrame.Content = innovationStack;
        ModalResearchContent.Children.Add(innovationFrame);

        // Academic Leadership
        var leadershipFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var leadershipStack = new VerticalStackLayout { Spacing = 10 };
        leadershipStack.Children.Add(new Label
        {
            Text = "Academic Leadership",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var lead1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var lead1Stack = new VerticalStackLayout { Spacing = 5 };
        lead1Stack.Children.Add(new Label { Text = "Department Head", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        lead1Stack.Children.Add(new Label { Text = "Department Head of Engineering and Computer Science at McNeese State University since 2019. Oversees programs in chemical, mechanical, civil, electrical, and computer engineering.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        lead1Frame.Content = lead1Stack;
        leadershipStack.Children.Add(lead1Frame);
        
        var lead2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var lead2Stack = new VerticalStackLayout { Spacing = 5 };
        lead2Stack.Children.Add(new Label { Text = "Curriculum Development", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        lead2Stack.Children.Add(new Label { Text = "Developed the 'Natural Gas Processing Fundamentals' course, designed to bridge knowledge gaps for both chemical and non-chemical engineering students, reflecting commitment to interdisciplinary education.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        lead2Frame.Content = lead2Stack;
        leadershipStack.Children.Add(lead2Frame);
        
        var lead3Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var lead3Stack = new VerticalStackLayout { Spacing = 5 };
        lead3Stack.Children.Add(new Label { Text = "Mentorship and Industry Collaboration", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        lead3Stack.Children.Add(new Label { Text = "Collaborates with industry professionals to mentor students on senior design capstone projects, ensuring practical, real-world experience is integrated into the engineering curriculum.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        lead3Frame.Content = lead3Stack;
        leadershipStack.Children.Add(lead3Frame);
        
        leadershipFrame.Content = leadershipStack;
        ModalResearchContent.Children.Add(leadershipFrame);

        // Recognition
        var recognitionFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var recognitionStack = new VerticalStackLayout { Spacing = 10 };
        recognitionStack.Children.Add(new Label
        {
            Text = "Recognition and Professional Development",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var rec1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var rec1Stack = new VerticalStackLayout { Spacing = 5 };
        rec1Stack.Children.Add(new Label { Text = "Leadership Development", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        rec1Stack.Children.Add(new Label { Text = "Selected to participate in the University of Louisiana System's Management & Leadership Institute, preparing faculty for executive-level roles within academia.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        rec1Frame.Content = rec1Stack;
        recognitionStack.Children.Add(rec1Frame);
        
        var rec2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var rec2Stack = new VerticalStackLayout { Spacing = 5 };
        rec2Stack.Children.Add(new Label { Text = "Academic Impact", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        rec2Stack.Children.Add(new Label { Text = "Over 25 publications and more than 10,000 reads on ResearchGate, sharing expertise widely and impacting both academic and industrial communities.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        rec2Frame.Content = rec2Stack;
        recognitionStack.Children.Add(rec2Frame);
        
        recognitionFrame.Content = recognitionStack;
        ModalResearchContent.Children.Add(recognitionFrame);

    }

    private void PopulateGarnerResearchContent()
    {
        // Professional Background
        var backgroundFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var backgroundStack = new VerticalStackLayout { Spacing = 10 };
        backgroundStack.Children.Add(new Label
        {
            Text = "Professional Background",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var bgFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var bgStack = new VerticalStackLayout { Spacing = 5 };
        bgStack.Children.Add(new Label { Text = "Education & Experience", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        bgStack.Children.Add(new Label { Text = "B.S. in Electrical Engineering from McNeese State University", FontSize = 14, TextColor = Colors.White });
        bgStack.Children.Add(new Label { Text = "M.S. in Electrical Engineering from Southern Methodist University", FontSize = 14, TextColor = Colors.White });
        bgStack.Children.Add(new Label { Text = "Over 30 years of teaching experience at McNeese", FontSize = 14, TextColor = Colors.White });
        bgStack.Children.Add(new Label { Text = "Industrial career spanning about a decade in defense electronics", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        bgStack.Children.Add(new Label { Text = "Worked for Honeywell in New Mexico and Raytheon in Texas", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        bgFrame.Content = bgStack;
        backgroundStack.Children.Add(bgFrame);
        
        backgroundFrame.Content = backgroundStack;
        ModalResearchContent.Children.Add(backgroundFrame);

        // Teaching Areas
        var teachingFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var teachingStack = new VerticalStackLayout { Spacing = 10 };
        teachingStack.Children.Add(new Label
        {
            Text = "Teaching Areas",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        teachingStack.Children.Add(new Label { Text = "• Microcontrollers", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        teachingStack.Children.Add(new Label { Text = "• Internet of Things (IoT)", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        teachingStack.Children.Add(new Label { Text = "• Analog Electronics", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        teachingStack.Children.Add(new Label { Text = "• Programmable Logic Controllers (PLCs)", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        teachingStack.Children.Add(new Label { Text = "• Senior Projects Courses", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        teachingFrame.Content = teachingStack;
        ModalResearchContent.Children.Add(teachingFrame);

        // Professional Involvement
        var involvementFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var involvementStack = new VerticalStackLayout { Spacing = 10 };
        involvementStack.Children.Add(new Label
        {
            Text = "Professional Involvement",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var invFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var invStack = new VerticalStackLayout { Spacing = 5 };
        invStack.Children.Add(new Label { Text = "IEEE Member & Faculty Advisor", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        invStack.Children.Add(new Label { Text = "Member of the IEEE (Institute of Electrical and Electronics Engineers)", FontSize = 14, TextColor = Colors.White });
        invStack.Children.Add(new Label { Text = "Current faculty advisor for the McNeese IEEE student branch", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        invFrame.Content = invStack;
        involvementStack.Children.Add(invFrame);
        
        involvementFrame.Content = involvementStack;
        ModalResearchContent.Children.Add(involvementFrame);

    }

    private void PopulateSinghResearchContent()
    {
        // Research Focus
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
        researchFocusStack.Children.Add(new Label { Text = "• Physics-Aware Perception for Robot Physical Intelligence", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Full-Stack Performance Engineering for Edge Robots", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Differentiable Physics Integration with 3D Vision", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusStack.Children.Add(new Label { Text = "• Efficient Computing Strategies for Resource-Constrained Systems", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        researchFocusFrame.Content = researchFocusStack;
        ModalResearchContent.Children.Add(researchFocusFrame);

        // Research Overview
        var overviewFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var overviewStack = new VerticalStackLayout { Spacing = 10 };
        overviewStack.Children.Add(new Label
        {
            Text = "Research Overview",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var overFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var overStack = new VerticalStackLayout { Spacing = 5 };
        overStack.Children.Add(new Label { Text = "Advancing Robot Physical Intelligence", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        overStack.Children.Add(new Label { Text = "Research focuses on advancing robot physical intelligence through physics-aware perception and full-stack performance engineering. Investigates integrating differentiable physics with 3D vision to enable robots to predict dynamic interactions in real time. Also works on developing efficient computing strategies for resource-constrained edge robots.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        overFrame.Content = overStack;
        overviewStack.Children.Add(overFrame);
        
        overviewFrame.Content = overviewStack;
        ModalResearchContent.Children.Add(overviewFrame);

        // Ongoing Work
        var ongoingFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var ongoingStack = new VerticalStackLayout { Spacing = 10 };
        ongoingStack.Children.Add(new Label
        {
            Text = "Ongoing Research",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var ong1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var ong1Stack = new VerticalStackLayout { Spacing = 5 };
        ong1Stack.Children.Add(new Label { Text = "Sensitivity Guided Mixed-Precision Quantization", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        ong1Stack.Children.Add(new Label { Text = "For tiny vision models to optimize performance and resource efficiency", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        ong1Frame.Content = ong1Stack;
        ongoingStack.Children.Add(ong1Frame);
        
        var ong2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var ong2Stack = new VerticalStackLayout { Spacing = 5 };
        ong2Stack.Children.Add(new Label { Text = "Physics-Aware Reinforcement Learning Framework", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        ong2Stack.Children.Add(new Label { Text = "For object localization using physics-based models", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        ong2Frame.Content = ong2Stack;
        ongoingStack.Children.Add(ong2Frame);
        
        ongoingFrame.Content = ongoingStack;
        ModalResearchContent.Children.Add(ongoingFrame);

        // Publications
        var publicationsFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var publicationsStack = new VerticalStackLayout { Spacing = 10 };
        publicationsStack.Children.Add(new Label
        {
            Text = "Recent Publications",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var pub1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var pub1Stack = new VerticalStackLayout { Spacing = 5 };
        pub1Stack.Children.Add(new Label { Text = "Physics-Guided Fusion for Robust 3D Tracking", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        pub1Stack.Children.Add(new Label { Text = "Singh, P. R., Gottumukkala, R., Maida, A. S., Barhorst, A. B., & Gopu, V. (2025). Physics-Guided Fusion for Robust 3D Tracking of Fast Moving Small Objects. arXiv preprint arXiv:2510.20126.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        pub1Frame.Content = pub1Stack;
        publicationsStack.Children.Add(pub1Frame);
        
        var pub2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var pub2Stack = new VerticalStackLayout { Spacing = 5 };
        pub2Stack.Children.Add(new Label { Text = "Analysis of Kalman Filter based Object Tracking Methods", FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        pub2Stack.Children.Add(new Label { Text = "Singh, P. R., Gottumukkala, R., & Maida, A. (2025). An Analysis of Kalman Filter based Object Tracking Methods for Fast-Moving Tiny Objects. arXiv preprint arXiv:2509.18451.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        pub2Frame.Content = pub2Stack;
        publicationsStack.Children.Add(pub2Frame);
        
        publicationsFrame.Content = publicationsStack;
        ModalResearchContent.Children.Add(publicationsFrame);

        // Areas of Focus
        var areasFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var areasStack = new VerticalStackLayout { Spacing = 10 };
        areasStack.Children.Add(new Label
        {
            Text = "Areas of Focus",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        areasStack.Children.Add(new Label { Text = "• 3D Computer Vision", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        areasStack.Children.Add(new Label { Text = "• Robotics", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        areasStack.Children.Add(new Label { Text = "• Optimization", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        areasFrame.Content = areasStack;
        ModalResearchContent.Children.Add(areasFrame);

    }

    private void PopulateAndersonResearchContent()
    {
        // Areas of Specialization
        var specializationFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var specializationStack = new VerticalStackLayout { Spacing = 10 };
        specializationStack.Children.Add(new Label
        {
            Text = "Areas of Specialization",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        specializationStack.Children.Add(new Label { Text = "• Web Development", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        specializationStack.Children.Add(new Label { Text = "• Operating Systems Administration", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        specializationStack.Children.Add(new Label { Text = "• Programming Languages (Python, C, Java)", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        specializationStack.Children.Add(new Label { Text = "• Embedded Systems", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        specializationStack.Children.Add(new Label { Text = "• Computer Hardware", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        specializationFrame.Content = specializationStack;
        ModalResearchContent.Children.Add(specializationFrame);

        // Research Topics
        var researchTopicsFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var researchTopicsStack = new VerticalStackLayout { Spacing = 10 };
        researchTopicsStack.Children.Add(new Label
        {
            Text = "Research Topics",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var topic1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var topic1Stack = new VerticalStackLayout { Spacing = 5 };
        topic1Stack.Children.Add(new Label { Text = "FPGA's (Field-Programmable Gate Arrays) and Security", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        topic1Stack.Children.Add(new Label { Text = "Research focuses on the intersection of FPGA technology and security applications, exploring how programmable gate arrays can be utilized to enhance system security and performance.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        topic1Frame.Content = topic1Stack;
        researchTopicsStack.Children.Add(topic1Frame);
        
        var topic2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var topic2Stack = new VerticalStackLayout { Spacing = 5 };
        topic2Stack.Children.Add(new Label { Text = "Integrating Edge Computing with AI", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        topic2Stack.Children.Add(new Label { Text = "Exploring the integration of edge computing technologies with artificial intelligence to enable efficient, low-latency AI applications at the network edge.", FontSize = 12, TextColor = Color.FromArgb("#CCCCCC"), LineBreakMode = LineBreakMode.WordWrap });
        topic2Frame.Content = topic2Stack;
        researchTopicsStack.Children.Add(topic2Frame);
        
        researchTopicsFrame.Content = researchTopicsStack;
        ModalResearchContent.Children.Add(researchTopicsFrame);

    }

    private void PopulateZhangResearchContent()
    {
        // Research Overview
        var overviewFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var overviewStack = new VerticalStackLayout { Spacing = 10 };
        overviewStack.Children.Add(new Label
        {
            Text = "Research Overview",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        overviewStack.Children.Add(new Label 
        { 
            Text = "Dr. Zhang has worked with and mentored engineering students on many undergraduate research and capstone projects.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        overviewFrame.Content = overviewStack;
        ModalResearchContent.Children.Add(overviewFrame);

        // Research Focus
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
        researchFocusStack.Children.Add(new Label 
        { 
            Text = "• Computational Fluid Dynamics (CFD) for Flooding Damage Simulation", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        researchFocusStack.Children.Add(new Label 
        { 
            Text = "• Hurricane Wind Impact Analysis on Industrial Facilities", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        researchFocusStack.Children.Add(new Label 
        { 
            Text = "• Pollutant Transport and Environmental Assessments", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        researchFocusFrame.Content = researchFocusStack;
        ModalResearchContent.Children.Add(researchFocusFrame);

        // Student Involvement
        var studentFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var studentStack = new VerticalStackLayout { Spacing = 10 };
        studentStack.Children.Add(new Label
        {
            Text = "Student Involvement",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        studentStack.Children.Add(new Label 
        { 
            Text = "Seven mechanical and civil engineering students were directly involved in these research projects. The projects resulted in multiple research papers published and presented at The American Society of Mechanical Engineers (ASME) international conferences, including the 2023 ASME International Mechanical Engineering Congress and Exposition in New Orleans, Louisiana, and the 2024 ASME Fluids Engineering Summer Conference in Anaheim, California.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        studentFrame.Content = studentStack;
        ModalResearchContent.Children.Add(studentFrame);

        // Professional Service
        var serviceFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var serviceStack = new VerticalStackLayout { Spacing = 10 };
        serviceStack.Children.Add(new Label
        {
            Text = "Professional Service",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        serviceStack.Children.Add(new Label 
        { 
            Text = "Dr. Zhang is involved with National ASME. He currently serves on the ASME Fluids Engineering Division Executive Committee as Secretary for 2023-24, and will move to Vice Chair for 2024-25. He is also the ASME academic advisor for McNeese State University.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        serviceFrame.Content = serviceStack;
        ModalResearchContent.Children.Add(serviceFrame);
    }

    private void PopulateXieResearchContent()
    {
        // Research Overview
        var overviewFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var overviewStack = new VerticalStackLayout { Spacing = 10 };
        overviewStack.Children.Add(new Label
        {
            Text = "Research Overview",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        overviewStack.Children.Add(new Label 
        { 
            Text = "Dr. Xie joined McNeese State University in 2017 and her research areas include wireless communication, wireless networks, software engineering and algorithm design and analysis.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        overviewFrame.Content = overviewStack;
        ModalResearchContent.Children.Add(overviewFrame);

        // Infrastructure Development
        var infrastructureFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var infrastructureStack = new VerticalStackLayout { Spacing = 10 };
        infrastructureStack.Children.Add(new Label
        {
            Text = "Infrastructure Development",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        
        var lab1Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var lab1Stack = new VerticalStackLayout { Spacing = 5 };
        lab1Stack.Children.Add(new Label { Text = "Network and Security Lab", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#FFD204") });
        lab1Stack.Children.Add(new Label { Text = "Established with a $91,000 Board of Regents enhancement fund awarded in 2019.", FontSize = 14, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        lab1Frame.Content = lab1Stack;
        infrastructureStack.Children.Add(lab1Frame);

        var lab2Frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#002a54"),
            CornerRadius = 6,
            Padding = 12,
            HasShadow = false
        };
        var lab2Stack = new VerticalStackLayout { Spacing = 5 };
        lab2Stack.Children.Add(new Label { Text = "Cybersecurity Fund", FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#00aeef") });
        lab2Stack.Children.Add(new Label { Text = "Secured a $105,000 Board of Regents Cybersecurity fund in 2023, which she is currently utilizing to upgrade the Network and Security Lab and Computer Lab.", FontSize = 14, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap });
        lab2Frame.Content = lab2Stack;
        infrastructureStack.Children.Add(lab2Frame);

        infrastructureFrame.Content = infrastructureStack;
        ModalResearchContent.Children.Add(infrastructureFrame);

        // Student Mentorship
        var mentorshipFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var mentorshipStack = new VerticalStackLayout { Spacing = 10 };
        mentorshipStack.Children.Add(new Label
        {
            Text = "Student Mentorship",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        mentorshipStack.Children.Add(new Label 
        { 
            Text = "Dr. Xie mentors NASA LaSPACE senior design and the NASA LaACES Balloon undergraduate research projects.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        mentorshipFrame.Content = mentorshipStack;
        ModalResearchContent.Children.Add(mentorshipFrame);

        // Professional Service
        var serviceFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var serviceStack = new VerticalStackLayout { Spacing = 10 };
        serviceStack.Children.Add(new Label
        {
            Text = "Professional Service",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        serviceStack.Children.Add(new Label 
        { 
            Text = "Dr. Xie's commitment to service is exemplary. She served as a co-chair for 2023 E-Week and as the 2024 E-Week chair. Moreover, she has been an active member of various university committees including Faculty Senate, Academic Affairs Committee, Community Partnership and Town Hall Committee and Academic Advising Committee.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        serviceFrame.Content = serviceStack;
        ModalResearchContent.Children.Add(serviceFrame);

        // Recognition
        var recognitionFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var recognitionStack = new VerticalStackLayout { Spacing = 10 };
        recognitionStack.Children.Add(new Label
        {
            Text = "Recognition",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        recognitionStack.Children.Add(new Label 
        { 
            Text = "All her endeavors were recognized by the Industry Advisory Board with the Outstanding Faculty 2023-2024 award.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#FFD204"),
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.WordWrap
        });
        recognitionFrame.Content = recognitionStack;
        ModalResearchContent.Children.Add(recognitionFrame);
    }

    private void PopulateGuoResearchContent()
    {
        // Research Overview
        var overviewFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var overviewStack = new VerticalStackLayout { Spacing = 10 };
        overviewStack.Children.Add(new Label
        {
            Text = "Research Overview",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        overviewStack.Children.Add(new Label 
        { 
            Text = "Dr. Qi Guo's research interest lies in conducting comprehensive energy audits in oil refining plants, manufacturing sectors, or multi-zone buildings.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        overviewFrame.Content = overviewStack;
        ModalResearchContent.Children.Add(overviewFrame);

        // Energy Systems Analysis
        var systemsFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var systemsStack = new VerticalStackLayout { Spacing = 10 };
        systemsStack.Children.Add(new Label
        {
            Text = "Energy Systems Analysis",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        systemsStack.Children.Add(new Label 
        { 
            Text = "This involves analyzing various energy systems within the plant or building, including:", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        systemsStack.Children.Add(new Label { Text = "• Energy supply system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Lighting system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Motor system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Pumping and fan system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Compressed air system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Process heating system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Process cooling system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• HVAC system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsStack.Children.Add(new Label { Text = "• Co-generation system", FontSize = 14, TextColor = Color.FromArgb("#CCCCCC") });
        systemsFrame.Content = systemsStack;
        ModalResearchContent.Children.Add(systemsFrame);

        // Project Approach
        var approachFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var approachStack = new VerticalStackLayout { Spacing = 10 };
        approachStack.Children.Add(new Label
        {
            Text = "Project Approach",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        approachStack.Children.Add(new Label 
        { 
            Text = "The project timeline is structured to encompass a comprehensive plant energy baseline analysis, an on-site visit for data collection, the identification and quantification of energy-saving opportunities, and the development of tailored energy-saving recommendations.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        approachFrame.Content = approachStack;
        ModalResearchContent.Children.Add(approachFrame);

        // Expected Outcomes
        var outcomesFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var outcomesStack = new VerticalStackLayout { Spacing = 10 };
        outcomesStack.Children.Add(new Label
        {
            Text = "Expected Outcomes",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        outcomesStack.Children.Add(new Label 
        { 
            Text = "The expected project outcomes include the identification of cost-effective energy saving measures and the promotion of improved sustainability practices within the chemical processing and oil refining plant.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        outcomesFrame.Content = outcomesStack;
        ModalResearchContent.Children.Add(outcomesFrame);
    }

    private void PopulateRostiResearchContent()
    {
        // Research Overview
        var overviewFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var overviewStack = new VerticalStackLayout { Spacing = 10 };
        overviewStack.Children.Add(new Label
        {
            Text = "Research Overview",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        overviewStack.Children.Add(new Label 
        { 
            Text = "Dr. Firouz Rosti's research in soil mechanics and geotechnical engineering focuses on soil-structure interactions, pile capacity, and soil properties such as setup and thixotropy in deep foundation systems.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        overviewFrame.Content = overviewStack;
        ModalResearchContent.Children.Add(overviewFrame);

        // Research Focus Areas
        var focusFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var focusStack = new VerticalStackLayout { Spacing = 10 };
        focusStack.Children.Add(new Label
        {
            Text = "Research Focus Areas",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        focusStack.Children.Add(new Label 
        { 
            Text = "His work examines the behavior of various soil types under different loading conditions and environmental factors, aiming to improve construction stability through innovative soil stabilization techniques. By using materials like nanomaterials, biopolymers, and industrial by-products, Dr. Rosti enhances soil strength while promoting environmental sustainability through recycling.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        focusFrame.Content = focusStack;
        ModalResearchContent.Children.Add(focusFrame);

        // Laboratory Facilities
        var labFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var labStack = new VerticalStackLayout { Spacing = 10 };
        labStack.Children.Add(new Label
        {
            Text = "Laboratory Facilities",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        labStack.Children.Add(new Label 
        { 
            Text = "At McNeese State University, Dr. Rosti leads a state-of-the-art soils lab, conducting comprehensive soil testing, material characterization, and model simulations. He evaluates pile capacity in clay soils and studies how it increases over time after installation, providing valuable insights for foundation design in complex environments.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        labFrame.Content = labStack;
        ModalResearchContent.Children.Add(labFrame);

        // Climate Change Research
        var climateFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#003087"),
            CornerRadius = 8,
            Padding = 15,
            HasShadow = false
        };
        var climateStack = new VerticalStackLayout { Spacing = 10 };
        climateStack.Children.Add(new Label
        {
            Text = "Climate Change Impact Research",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });
        climateStack.Children.Add(new Label 
        { 
            Text = "Committed to understanding the impact of climate change on soil properties and geotechnical systems, Dr. Rosti's research aims to mitigate risks such as landslides, soil erosion, and infrastructure failures, advancing geotechnical engineering and sustainable practices.", 
            FontSize = 14, 
            TextColor = Color.FromArgb("#CCCCCC"),
            LineBreakMode = LineBreakMode.WordWrap
        });
        climateFrame.Content = climateStack;
        ModalResearchContent.Children.Add(climateFrame);
    }

    private void OnCloseModalClicked(object sender, EventArgs e)
    {
        ResearchModalOverlay.IsVisible = false;
    }

    private void OnModalBackgroundTapped(object sender, EventArgs e)
    {
        ResearchModalOverlay.IsVisible = false;
    }

    // Coordinators Section
    private void OnAghiliExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Matthew Aghili");
    }

    private void OnDermisisExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Dimitrios Dermisis");
    }

    private void OnGarnerExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Mr. Brent Garner");
    }

    private void OnLavergneExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Jennifer Lavergne");
    }

    private void OnLiExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Zhuang Li");
    }

    private void OnSubramaniamExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Ramalingam Subramaniam");
    }

    // Faculty Section (sorted by last name)
    private void OnAmbatipatiFacultyExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Vasan Ambatipati");
    }

    private void OnAndersonExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Ms. Rhonda Anderson");
    }

    private void OnGuoExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Qi Guo");
    }

    private void OnLiuExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Qiu Liu");
    }

    private void OnMenonExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Vipin Menon");
    }

    private void OnRostiExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Firouz Rosti");
    }

    private void OnXieExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Bei Xie");
    }

    private void OnZhangExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Ning Zhang");
    }

    private void OnZeitounExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Dr. Zeyad Zeitoun");
    }

    private void OnSinghExpandButtonClicked(object sender, EventArgs e)
    {
        ShowResearchModal("Prithvi Raj Singh");
    }
}
