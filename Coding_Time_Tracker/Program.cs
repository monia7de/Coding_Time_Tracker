using System.Configuration;

namespace coding_time_tracker
{
    class Program
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new();
            databaseManager.CreateTable(connectionString);

            GetUserInput getUserInput = new();
            getUserInput.MainMenu();


        }
    }
}

