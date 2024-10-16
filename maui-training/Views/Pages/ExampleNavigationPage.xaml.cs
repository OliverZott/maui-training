namespace maui_training.Views.Pages;

public partial class ExampleNavigationPage : ContentPage
{
    public ExampleNavigationPage()
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

    private async void OnUserInputButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(UserInputPage));
    }
}
