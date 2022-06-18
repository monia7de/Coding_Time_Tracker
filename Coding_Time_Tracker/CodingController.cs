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
        /// Method <c>GetByToday</c> retrieves all records for today and displays them in a table
        /// </summary>
        /// <param name="today"></param>
        /// <returns>List<Habit></returns>
        internal List<Habit> GetByToday()
        {
            var today = DateTime.Today;
            string day = today.ToString("dd");
            string year = today.ToString("yyyy");
            string month = today.ToString("MM");

            List<Habit> tableDataByToday = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM habits WHERE strftime('%Y', Date) = '{year}' AND strftime('%m', Date) = '{month}'AND strftime('%d', Date) = '{day}'";


                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableDataByToday.Add(
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

            TableVisualisation.ShowTable(tableDataByToday);

            return tableDataByToday;

        }
        /// <summary>
        /// Method <c>GetByThisWeek</c>retrieves all records for the last 7 days and displays them in a table
        /// </summary>
        /// <returns>List<Habit></returns>
        internal List<Habit> GetByThisWeek()
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);

            var todayString = today.ToString("yyyy-MM-dd");
            var lastWeekString = lastWeek.ToString("yyyy-MM-dd");

            
            List<Habit> tableDataByThisWeek = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM habits WHERE strftime('%Y-%m-%d', Date) BETWEEN '{lastWeekString}' AND '{todayString}'";
                        
                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableDataByThisWeek.Add(
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

            TableVisualisation.ShowTable(tableDataByThisWeek);

            return tableDataByThisWeek;

        }


        /// <summary>
        /// Method <c>GetByDay</c> retrieves all records for a selected day of a given month and year and displays them in a table
        /// </summary>
        /// <param name="day"></param>
        /// <returns>List<Habit></returns>
        internal List<Habit> GetByDay(DateTime yearMonthDay)
        {
            
            string year = yearMonthDay.ToString("yyyy");
            string month = yearMonthDay.ToString("MM");
            string day = yearMonthDay.ToString("dd");

            List<Habit> tableDataByDay = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM habits WHERE strftime('%Y', Date) = '{year}' AND strftime('%m', Date) = '{month}'AND strftime('%d', Date) = '{day}'";
                   

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableDataByDay.Add(
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

            TableVisualisation.ShowTable(tableDataByDay);

            return tableDataByDay;
        }


        /// <summary>
        /// Method <c>GetByMonthAndYear</c> retrieves all records for a selected month of a given year and displays them in a table
        /// </summary>
        /// <param name="monthAndYear"></param>
        /// <returns>List<Habit></returns>
        internal List<Habit> GetByMonthAndYear(DateTime yearAndMonth)
        {
          
            string year = yearAndMonth.ToString("yyyy");
            string month = yearAndMonth.ToString("MM");

            List <Habit> tableDataByMonthAndYear = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM habits WHERE strftime('%Y', Date) = '{year}' AND strftime('%m', Date) = '{month}'";

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

        /// <summary>
        /// Method <c>GetByYear</c> retrieves all records for a selected year and displays them in a table
        /// </summary>
        /// <param name="year"></param>       
        /// <returns>List<Habit></returns>
        internal List<Habit> GetByYear(string year)
        {
            List<Habit> tableDataByYear = new List<Habit>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM habits WHERE strftime('%Y', Date) = '{year}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableDataByYear.Add(
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

            TableVisualisation.ShowTable(tableDataByYear);

            return tableDataByYear;
        }
    }
    
}