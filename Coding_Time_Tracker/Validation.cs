using System.Globalization;

namespace coding_time_tracker
{
   
    internal class Validation
    {
        GetUserInput getUserInput = new();

        /// <summary>
        /// Method <c>NullOrEmpty</c> validates that the input is not null or empty
        /// </summary>
        /// <returns>string</returns>
        internal string NullOrEmpty(string commandInput)
        {
            while (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nInvalid Command. Please type a number from 0 to 5. \n");
                commandInput = Console.ReadLine();

            }

            return commandInput;

        }
        /// <summary>
        /// Method <c>HabitNameInput</c> validates that the habit name is either 'c' or 'd'.
        /// </summary>
        /// <param name="habitNameInput"></param>
        /// <returns>string</returns>
        internal string HabitNameChoice(string? habitNameInput)
        {

            while (habitNameInput != "c" && habitNameInput != "d")
            {
                Console.WriteLine("\nInvalid Command. Please type 'c' or 'd' or type 0 to return to the Main Menu \n");
                habitNameInput = Console.ReadLine();

               if (habitNameInput == "0")
                {
                    getUserInput.MainMenu();
                }
               
            }
            return habitNameInput;


        }
        /// <summary>
        /// Method <c>DateInput</c> validates the date format
        /// </summary>
        /// <param name="dateInput"></param>
        /// <returns>string</returns>
        internal string DateInput(string? dateInput)
        {
            while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nNot a valid date. Please enter the date with the format: dd-mm-yyyy.\n\n");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        /// <summary>
        /// Method <c>DurationInput</c> validates the format of duration
        /// </summary>
        /// <param name="durationInput"></param>
        /// <returns>string</returns>
        internal string DurationInput(string durationInput)
        {
            while (!TimeSpan.TryParseExact(durationInput, @"h\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\n\nDuration invalid. Please enter the duration (Format: hh:mm) or type 0 to return to Main Menu\n\n");
                durationInput = Console.ReadLine();
                if (durationInput == "0")
                 {
                     getUserInput.MainMenu();
                 }
                 
            }
            return durationInput;   
        }

        /// <summary>
        /// Method <c>Time</c>validates the start and finish time inputs
        /// </summary>
        /// <param name="timeInput"></param>
        /// <returns>string</returns>
        internal TimeOnly Time(string timeInput) 
        {
            while (!TimeOnly.TryParseExact(timeInput, @"H\:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nTime invalid. Please enter the time again (Format: hh:mm) or type 0 to return to main menu\n\n");
                timeInput = Console.ReadLine();
                if (timeInput == "0")
                 {
                     getUserInput.MainMenu();
                 }
                
            }
            var time = TimeOnly.Parse(timeInput); 
            return time;
        }

        /// <summary>
        /// Method <c>Pomodoros</c> validates pomodoros input and converts it into an integer
        /// </summary>
        /// <param name="pomodorosInput"></param>
        /// <returns>int</returns>
        internal int Pomodoros(string? pomodorosInput)
        {
            while (!Int32.TryParse(pomodorosInput, out _) || string.IsNullOrEmpty(pomodorosInput) || Int32.Parse(pomodorosInput) < 0)
            {
                Console.WriteLine("\n You have to type a valid number or 0 to return to Main Menu. \n");
                pomodorosInput = Console.ReadLine();
            }
            var pomodoros = Int32.Parse(pomodorosInput);
           
            if (pomodoros == 0)
            {
                getUserInput.MainMenu();
            }
            
            return pomodoros;
        }

        /// <summary>
        /// Method <c>StartStopwatch</c> validates the start stopwatch input
        /// </summary>
        /// <param name="startInput"></param>
        /// <returns>string</returns>
        internal string StartStopwatch(string? startInput)
        {
            while (string.IsNullOrEmpty(startInput))
            {
                Console.WriteLine("\nInvalid Command. Please type 's' to start the Stopwatch or 0 to return to the Main Memu \n");
                startInput = Console.ReadLine();
                if (startInput == "0")
                 {
                     getUserInput.MainMenu();
                 }
                 
            }

            while (startInput != "s")
            {
                Console.WriteLine("\nInvalid Command.Please type 's' to start the Stopwatch or 0 to return to the Main Memu \n");
                startInput = Console.ReadLine();
                if (startInput == "0")
                 {
                     getUserInput.MainMenu();
                 }
                 

            }
            return startInput;
        }

        /// <summary>
        /// Method <c>EndStopwatch</c> validates end stopwatch input
        /// </summary>
        /// <param name="endInput"></param>
        /// <returns></returns>
        internal string EndStopwatch(string? endInput)
        {
            while (string.IsNullOrEmpty(endInput))
            {
                Console.WriteLine("\nInvalid Command. Please type 'es' to stop the Stopwatch or 0 to return to the Main Memu \n");
                endInput = Console.ReadLine();
                 if (endInput == "0")
                 {
                     getUserInput.MainMenu();
                 }
                 
            }

            while (endInput != "e")
            {
                Console.WriteLine("\nInvalid Command.Please type 'e' to stop the Stopwatch or 0 to return to the Main Memu \n");
                endInput = Console.ReadLine();
                if (endInput == "0")
                {
                    getUserInput.MainMenu();
                }
                
            }
            return endInput;
        }

        
        internal int ValidateMonth(string monthInput)
        {
            while (
                !Int32.TryParse(monthInput, out _)
                || string.IsNullOrEmpty(monthInput)
                || Int32.Parse(monthInput) < 0
                || Int32.Parse(monthInput) > 12)
            {
                Console.WriteLine("\nInvalid Month. Please type month(MM)\n");
                monthInput = Console.ReadLine();
            }

            var month = Int32.Parse(monthInput);


            return month;
        }

        internal int ValidateYear(string? yearInput)
        {
            while (
            !Int32.TryParse(yearInput, out _)
            || string.IsNullOrEmpty(yearInput)
            || Int32.Parse(yearInput) < 2000
            || Int32.Parse(yearInput) > 9999)
            {
                Console.WriteLine("\nInvalid Year. Please type year (a number from 2000 to 9999)\n");
                yearInput = Console.ReadLine();
            }

            var year = Int32.Parse(yearInput);

            return year;
        }
    }
}