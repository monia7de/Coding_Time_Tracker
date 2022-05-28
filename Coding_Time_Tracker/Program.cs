using System.Configuration;

namespace coding_time_tracker
{
    class Program
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new();
            databaseManager.CreateTable(connectionString);

            UserInput userInput = new();
            userInput.MainMenu();


        }
    }
}

