using Microsoft.Data.Sqlite;

namespace coding_time_tracker
{
    internal class DatabaseManager
    {
        internal void CreateTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {

                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS habits (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Date TEXT,
                    Duration TEXT,
                    )";

                    tableCmd.ExecuteNonQuery();
                }



            }
        }
    }
}
