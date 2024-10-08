# README

[Microsoft MAUI Tutorial](https://learn.microsoft.com/en-us/training/paths/build-apps-with-dotnet-maui/)

## Basic structure

- `App.xaml` used to define shared resources like styles and themes for your application (defined in Resource folder)
- `App.xaml.cs` code-behind file, containing the logic for application-level events and initialization. Represents your application at runtime. Include OnStart, OnResume, and OnSleep event handlers.

- `AppShell.xaml` defines the navigation structure of the app, including styling, URI based navigation, and options for layout, including flyout navigation and tabs for the application's root
- `AppShell.xaml.cs` code-behind file, handling navigation logic

- `MainPage.xaml` main user interface file where you define the layout and UI elements.
- `MainPage.xaml.cs` code-behind file, containing the logic for user interactions and UI updates
- `MainPageViewModel.cs` view model for the main page

- `MauiProgram.cs`  file to configure services, middleware, and other app-level settings. It’s similar to the Startup.cs file in ASP.NET Core and  entry point for the application. All platform-specific code calls the CreateMauiApp method of the static MauiProgram class in the end! You also register your services to **IoC/DI container** here `builder.Services.AddSingleton<IMyService, MyService>();`

At runtime, the app starts up in a platform-specific way and when initialization is complete, the platform-specific code calls the `MauiProgram.CreateMauiApp` method, which then creates and runs the `App` object

- `csproj`
	- `PropertyGroup` specifies the platform frameworks that the project targets, and items such as the application title, ID, version, display version, and supported operating systems.
	- `ItemGroup` specifies an image and color for the splash screen that appears while the app is loading, before the first window appears. You can also set the default locations for the fonts, images, and assets the app uses.

## Basic concept

### Shell

...the app's visual hierarchy is described in a class that subclasses the Shell class. This class can consist of three main hierarchical objects:

- `FlyoutItem` or `TabBar`
- `Tab` grouped content, navigable by bottom tabs
- `ShellContent` ContentPage objects for each tab

### Pages

...the root of the UI hierarchy in .NET MAUI inside of a Shell (e.g. class `MainPage`)

- `ContentPage` simplest and most common page type, simply displays its contents
- `TabbedPage` root page used for tab navigation. A tabbed page contains child page objects; one for each tab
- `FlyoutPage` contains a list of items. When you select an item, a view displaying the details for that item appears (master/detail style presentation)

### Views

A content page typically displays a view. A view enables you to retrieve and present data in a specific manner.

- `ContentView` default view for a content page, which displays items as-is
- `ScrollView` display items in a scrolling window
- `CarouselView` swipe through a collections
- `CollectionView` can retrieve data from a named data source and present each item using a template as a format

### Layouts and Controls

A view can contain a single control such as a **button**, **label**, or **text boxes** (views itself are controls)  

Controls are positioned in a layout. A layout defines the rules by which the controls are displayed relative to each other (layouts are also controls, sp you can add it to view).

- `StackLayout` as `HorizontalStackLayout` or `VerticalStackLayout`
- `AbsoluteLayout` exact coordinates for controls
- `FlexLayout` enables you to wrap the child controls it contains if they don't fit in a single row or column
- `Grid`