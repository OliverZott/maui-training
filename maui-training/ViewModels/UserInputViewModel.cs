using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_training.Models;

namespace maui_training.ViewModels;

internal partial class UserInputViewModel : ObservableObject
{
    private string? firstName;
    private string? lastName;
    private string? age;
    private ObservableCollection<User> users;

    public string FirstName
    {
        get => firstName;
        set => SetProperty(ref firstName, value);
    }
    public string LastName
    {
        get => lastName;
        set => SetProperty(ref lastName, value);
    }
    public string Age
    {
        get => age;
        set => SetProperty(ref age, value);
    }
    public ObservableCollection<User> Users
    {
        get => users;
        set => SetProperty(ref users, value);
    }



    public Command SaveUserCommand { get; }

    public UserInputViewModel()
    {
        InitializeUsers();
        SaveUserCommand = new Command(async () => await SaveUser());
    }

    public async void InitializeUsers()
    {
        Users = new ObservableCollection<User>(await App.SqliteDatabaseService.GetAllUsersAsync());
    }

    private async Task SaveUser()
    {
        if (string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Age))
        {
            // could be removed as soon as condition "canSaveUser" works in command instantiation
            await App.Current.MainPage.DisplayAlert("Error", "All fields are required.", "OK");

            return;
        }

        try
        {
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = int.Parse(Age)
            };

            await App.SqliteDatabaseService.CreateUserAsync(user);
            Users.Add(user);
            FirstName = LastName = Age = string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private bool CanSaveUser()
    {
        var canSave = !string.IsNullOrWhiteSpace(FirstName) ||
                      !string.IsNullOrWhiteSpace(LastName) ||
                      !string.IsNullOrWhiteSpace(Age);
        return canSave;
    }
}
