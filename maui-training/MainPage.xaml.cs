using maui_training.Views.Pages;

namespace maui_training;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnPhoneButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PhoneWordDialer));
    }

    private async void OnPhotoButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PhotoPage));
    }

    private async void OnTestButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TestPage));
    }
}
