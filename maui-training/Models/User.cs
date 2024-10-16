using SQLite;

namespace maui_training.Models;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
}

