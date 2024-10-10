using maui_training.Helpers;

namespace maui_training.Views.Pages;

public partial class PhoneWordDialer : ContentPage
{
    private string? translatedNumber;

    public PhoneWordDialer()
    {
        InitializeComponent();
    }

    private void OnTranslate(object sender, EventArgs e)
    {
        string enterdNumber = PhoneNumberText.Text;
        translatedNumber = PhonewordTranslator.ToNumber(enterdNumber);
        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = $"Call {translatedNumber}";
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }

    async void OnCall(object sender, EventArgs e)
    {
        if (await DisplayAlert(
          "Dial a number", $"Would you like to call {translatedNumber}?", "Yes", "No"))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                {
                    PhoneDialer.Default.Open(translatedNumber);
                }
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number not valid.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}
