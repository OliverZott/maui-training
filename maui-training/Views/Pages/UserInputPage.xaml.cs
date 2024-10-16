using maui_training.Models;

namespace maui_training.Views.Pages;


// TODO: remove from code behind and use UserViewModel as DTO ?!?
// TODO: inject databaseservice and use DI
public partial class UserInputPage : ContentPage
{
    public UserInputPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        PeopleListView.ItemsSource = await App.SqliteDatabaseService.GetAllUsersAsync();
    }

    public async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var user = new User
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Age = int.Parse(AgeEntry.Text)
        };

        // TODO: specific convention App. ... instead of instantiation/DI ???
        await App.SqliteDatabaseService.CreateUserAsync(user);

        // Clear input form
        FirstNameEntry.Text = LastNameEntry.Text = AgeEntry.Text = string.Empty;

        // Refresh View
        PeopleListView.ItemsSource = await App.SqliteDatabaseService.GetAllUsersAsync();

    }
}
