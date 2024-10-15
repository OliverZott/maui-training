using maui_training.ViewModels;

namespace maui_training.Views.Pages;

public partial class CollectionExamplePage : ContentPage
{
    public CollectionExamplePage()
    {
        InitializeComponent();

        BindingContext = new UserViewModel();
    }
}
