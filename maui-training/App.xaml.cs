using maui_training.Services;

namespace maui_training;

public partial class App : Application
{
    static SqliteDatabaseService? sqliteDatabaseService;
    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "user.db3");

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    internal static SqliteDatabaseService SqliteDatabaseService
    {

        get
        {
            if (sqliteDatabaseService == null)
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "user.db3");
                sqliteDatabaseService = new SqliteDatabaseService(dbPath);
            }
            return sqliteDatabaseService;
        }
    }
}

