using System;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Collections.Generic;

namespace coding_time_tracker
{
    internal class CodingController
    {

        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        
        /// <summary>
        /// Method <c>Get</c> retrieves all records from the database
        /// </summary>
        /// <returns>List<Habit></Habit></returns>
        internal List<Habit> Get() 
        {
            List<Habit> tableData = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "SELECT * FROM habits";
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
                                        Name = reader.GetString(1),
                                        Date = reader.GetString(2),
                                        Duration = reader.GetString(3)
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
        /// <summary>
        /// Method <c>GetById</c> retrieves a habit record from the database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal Habit GetById(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM habits Where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        Habit habit = new();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            habit.Id = reader.GetInt32(0);
                            habit.Name = reader.GetString(1);
                            habit.Date = reader.GetString(2);
                            habit.Duration = reader.GetString(3);
                        }
                        Console.WriteLine("\n\n");

                        return habit;
                    };
                }
            }
        }

        /// <summary>
        /// Method <c>Post</c> adds a habit record to the database
        /// </summary>
        /// <param name="habit"></param>
        internal void Post(Habit habit)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"INSERT INTO habits (name, date, duration) VALUES ('{habit.Name}', '{habit.Date}', '{habit.Duration}')";

                    tableCmd.ExecuteNonQuery(); 
                }   
            }
        }

        /// <summary>
        /// Method <c>Update</c> updates a habit by id
        /// </summary>
        /// <param name="habit"></param>
        internal void Update(Habit habit)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText =
                        $@"UPDATE habits SET
                            Name = '{habit.Name}',
                            Date = '{habit.Date}',
                            Duration = '{habit.Duration}'
                            WHERE
                                Id = {habit.Id}
                            ";

                    tableCmd.ExecuteNonQuery();
                }

                
            }
        }
        /// <summary>
        /// Method <c>Delete</c> deletes a record from the database by id
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Method <c>GetByMonthAndYear</c> retrieves all records for a selected month of a given year
        /// </summary>
        /// <param name="monthAndYear"></param>
        /// <returns>List<Habit></returns>
        internal List<Habit> GetByMonthAndYear(DateTime yearAndMonth)
        {
            var nextMonth = yearAndMonth.AddMonths(1);

            List<Habit> tableDataByMonthAndYear = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = //$"SELECT * FROM habits WHERE Date >= '{yearAndMonth}' AND Date < '{nextMonth}'";
                                           //$"SELECT Id FROM habits WHERE MONTH(Date) = '{yearAndMonth.Month}' AND YEAR(Date)= '{yearAndMonth.Year}'";
                                           //$"SELECT * FROM habits WHERE strftime('%m', `Date`) = '{yearAndMonth.Month}'";
                                           //$"SELECT * from habits WHERE Date BETWEEN '{2022-04-01}' AND '{2022-04-31}'";
                                           $"SELECT * from habits WHERE Date BETWEEN '{yearAndMonth}' AND '{nextMonth}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableDataByMonthAndYear.Add(
                                    new Habit
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        Date = reader.GetString(2),
                                        Duration = reader.GetString(3)
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

            TableVisualisation.ShowTable(tableDataByMonthAndYear);

            return tableDataByMonthAndYear;
        }   
    }
}