using maui_training.ViewModels;

namespace maui_training.Views.Pages;

public partial class UserInputPage : ContentPage
{
    public UserInputPage()
    {
        InitializeComponent();
        BindingContext = new UserInputViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((UserInputViewModel)BindingContext).InitializeUsers();
    }
}
