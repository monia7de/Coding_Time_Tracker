﻿using Microsoft.Data.Sqlite;

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
                    @"CREATE TABLE IF NOT EXISTS coding_time (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Duration TEXT
                    )";

                    tableCmd.ExecuteNonQuery();
                }



            }
        }
    }
}
