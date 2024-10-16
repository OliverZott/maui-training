using maui_training.Models;
using SQLite;

namespace maui_training.Services;

internal class SqliteDatabaseService
{
    private readonly SQLiteAsyncConnection dataBase;

    public SqliteDatabaseService(string dbPath)
    {
        dataBase = new SQLiteAsyncConnection(dbPath);
        dataBase.CreateTableAsync<User>().Wait();
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        return dataBase.Table<User>().ToListAsync();
    }

    public Task<int> CreateUserAsync(User user)
    {
        return dataBase.InsertAsync(user);
    }
}
