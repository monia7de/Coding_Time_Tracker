using System;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Collections.Generic;

namespace coding_time_tracker
{
    internal class CodingController
    {

        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        

        internal List<Habit> Get() 
        {
            List<Habit> tableData = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "SELECT * FROM coding_time";
                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(
                                    new Habit
                                    {
                                        Id = reader.GetInt32(0),
                                        Date = reader.GetString(1),
                                        Duration = reader.GetString(2)
                                    });
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\nNo rows found.\n\n");
                        }

                    }
                }
                Console.WriteLine("\n\n");
            }

            TableVisualisation.ShowTable(tableData);

            return tableData;

        }


        internal Habit GetById(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM coding_time Where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        Habit coding = new();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            coding.Id = reader.GetInt32(0);
                            coding.Date = reader.GetString(1);
                            coding.Duration = reader.GetString(2);
                        }
                        Console.WriteLine("\n\n");

                        return coding;
                    };
                }
            }
        }


        internal void Post(Habit habit)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"INSERT INTO habits (date, duration) VALUES ('{habit.Date}', '{habit.Duration}')";

                    tableCmd.ExecuteNonQuery(); 
                }   
            }
        }

        internal void Update(Habit habit)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText =
                        $@"UPDATE coding_time SET
                            Date = '{habit.Date}',
                            Duration = '{habit.Duration}'
                            WHERE
                                Id = {habit.Id}
                            ";
                }
            }
        }

        internal void Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"DELETE from habits WHERE Id = '{id}'";
                    tableCmd.ExecuteNonQuery();

                    Console.WriteLine($"\n\nRecord with Id {id} was deleted. \n\n");
                }
            }
        }

     
    }
}