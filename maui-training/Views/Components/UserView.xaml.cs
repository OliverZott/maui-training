using maui_training.ViewModels;

namespace maui_training.Views.Components;

public partial class UserView : ContentView
{
    public UserView()
    {
        InitializeComponent();
        BindingContext = new UserViewModel();
    }
}
