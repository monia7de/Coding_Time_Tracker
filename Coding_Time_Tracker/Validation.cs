using System.Globalization;

namespace coding_time_tracker
{
    internal class Validation
    {
        GetUserInput getUnserInput = new();
        
        internal string NullOrEmpty(string commandInput)
        {
            while (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nInvalid Command. Please type a number from 0 to 5. \n");
                commandInput = Console.ReadLine();

            }

            return commandInput;

        }

        internal string DateInput(string dateInput)
        {
            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nNot a valid date. Please enter the date with the format: dd-mm-yy.\n\n");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }



        internal string HabitNameChoice(string habitNameInput)
        {
     
            while (habitNameInput != "c" && habitNameInput != "d")
            {
                Console.WriteLine("\nInvalid Command. Please type 'c' or 'd' or type 0 to return to the Main Menu \n");
                habitNameInput = Console.ReadLine();

                //if (habitNameInput == "0") getUserInput.MainMenu();
            }                
            return habitNameInput;       
                      
            
        }

        internal string DurationInput(string? durationInput)
        {
            while (!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\n\nDuration invalid. Please enter the duration (Format: hh:mm) or type 0 to return to main menu\n\n");
                durationInput = Console.ReadLine();
               // if (durationInput == "0") getUserInput.MainMenu();
            }
            return durationInput;   
        }

        internal string Time(string timeInput) //change TimeSpan i format
        {
            while (!TimeSpan.TryParseExact(timeInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\n\nTime invalid. Please enter the time again (Format: hh:mm) or type 0 to return to main menu\n\n");
                timeInput = Console.ReadLine();
               // if (timeInput == "0") getUserInput.MainMenu();
            }
            return timeInput;
        }

        internal int Pomodoros(string? pomodorosInput)
        {
            while (!Int32.TryParse(pomodorosInput, out _) || string.IsNullOrEmpty(pomodorosInput) || Int32.Parse(pomodorosInput) < 0)
            {
                Console.WriteLine("\n You have to type a valid number or 0 to return to Main Menu. \n");
                pomodorosInput = Console.ReadLine();
            }
            var pomodoros = Int32.Parse(pomodorosInput);

            //if (pomodoros == 0) getUserInput.MainMenu();

            return pomodoros;
        }

        internal string StartStopwatch(string? startInput)
        {
            while (string.IsNullOrEmpty(startInput))
            {
                Console.WriteLine("\nInvalid Command. Please type 's' to start the Stopwatch or 0 to return to the Main Memu \n");
                startInput = Console.ReadLine();
               // if (startInput == "0") getUserInput.MainMenu();
            }

            while (startInput != "s")
            {
                Console.WriteLine("\nInvalid Command.Please type 's' to start the Stopwatch or 0 to return to the Main Memu \n");
                startInput = Console.ReadLine();
               // if (startInput == "0") getUserInput.MainMenu();
            }
            return startInput;
        }

        internal string EndStopwatch(string? endInput)
        {
            while (string.IsNullOrEmpty(endInput))
            {
                Console.WriteLine("\nInvalid Command. Please type 'es' to stop the Stopwatch or 0 to return to the Main Memu \n");
                endInput = Console.ReadLine();
              //  if (endInput == "0") getUserInput.MainMenu();
            }

            while (endInput != "e")
            {
                Console.WriteLine("\nInvalid Command.Please type 'e' to stop the Stopwatch or 0 to return to the Main Memu \n");
                endInput = Console.ReadLine();
              //  if (endInput == "0") getUserInput.MainMenu();
            }
            return endInput;
        }
    }
}