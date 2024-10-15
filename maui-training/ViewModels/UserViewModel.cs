using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_training.Models;


namespace maui_training.ViewModels;

partial class UserViewModel : ObservableObject
{
    private ObservableCollection<User> _users;

    public ObservableCollection<User> Users
    {
        get => _users;
        set => SetProperty(ref _users, value);
    }

    public UserViewModel()
    {
        _users =
        [
            new() { FirstName = "Jane", LastName = "Smith" },
            new() { FirstName = "John", LastName = "Doe" },
            new() { FirstName = "Jane2", LastName = "Smith" },
            new() { FirstName = "John2", LastName = "Doe" },
            new() { FirstName = "Jane3", LastName = "Smith" },
            new() { FirstName = "John3", LastName = "Doe" },
        ];
    }
}
