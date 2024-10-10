using maui_training.Views.Pages;

namespace maui_training;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(PhoneWordDialer), typeof(PhoneWordDialer));
        Routing.RegisterRoute(nameof(TestPage), typeof(TestPage));
        Routing.RegisterRoute(nameof(PhotoPage), typeof(PhotoPage));
    }
}
