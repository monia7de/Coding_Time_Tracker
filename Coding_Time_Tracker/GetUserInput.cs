using System;
using System.Globalization;
using System.Diagnostics;



namespace coding_time_tracker
{
    internal class GetUserInput
    {
        CodingController codingController = new();
        static Validation validation = new();
        DurationCalculator durationCalculator = new();
        
        /// <summary>
        /// Method <c>MainMenu</c> gets user input for the main menu choice, validates that the input is not null or empty,
        /// then calls a method for the selected choice.
        /// </summary>
        internal void MainMenu()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\n What would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View All Records.");
                Console.WriteLine("Type 2 to Insert Record.");
                Console.WriteLine("Type 3 to Update Record.");
                Console.WriteLine("Type 4 to Delete Record.");
                Console.WriteLine("Type 5 to View Daily/ Weekly/ Monthly/ Yearly Report.");
                Console.WriteLine("--------------------------------------\n");

                string commandInput = Console.ReadLine();

                string inputValidated = validation.NullOrEmpty(commandInput);

             
                switch (inputValidated)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        codingController.Get();
                        break;

                    case "2":
                         ProcessAdd();
                        break;

                    case "3":
                         ProcessUpdate();
                        break;

                    case "4":
                          ProcessDelete();
                        break;
                    case "5":
                        ProcessReport();
                        break;

                    case "default":
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 5. \n");
                        break;

                }

            }

        }
        /// <summary>
        /// Method <c>ProcessAdd</c> gets user input for the properties of a habit 
        /// and passes it to the codingController method to be added to the database.
        /// </summary>
        private void ProcessAdd()
        {
            var name = GetHabitNameInput();
            var date = GetDateInput();
            var duration = GetDurationInput();

            Habit habit = new();

            habit.Name = name;
            habit.Date = date;            
            habit.Duration = duration;

            codingController.Post(habit);
                              
        } 
        /// <summary>
        /// Method <c>ProcessUpdate</c> displays all records, gets the user input that is to be updated,
        /// validates that the input is not null or empty and that it can be converted into an int,
        /// then passes the updated habit to the Update method in the codingController
        /// </summary>
        private void ProcessUpdate()
        {
            codingController.Get();
            Console.WriteLine("Please add id of the record you want to update (or type 0 to return to Main Menu");
            string commandInput = Console.ReadLine();   

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("\nYou have to type an Id (or 0 to return to Main Menu).\n");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var habit = codingController.GetById(id);  

            while (habit.Id == 0)
            {
                Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
                ProcessUpdate();

            }

            var updateInput = "";
            bool updating = true;
            while (updating == true)
            {
                Console.WriteLine($"\nType 'n' for Name \n");
                Console.WriteLine($"\nType 'd' for Date \n");
                Console.WriteLine($"\nType 't' for Duration \n");
               //Console.WriteLine($"\nType 't' for StartTime \n");    + create cases & methods
               //Console.WriteLine($"\nType 't' for EndTime \n");
               //Console.WriteLine($"\nType 't' for Pomodoros \n");
                Console.WriteLine($"\nType 's' to save update \n");
                Console.WriteLine($"\nType '0' to go back to the Main Menu \n");
                
                updateInput = Console.ReadLine();

                switch (updateInput)
                {
                    case "n":
                        habit.Name = GetHabitNameInput();
                        break;

                    case "d":
                        habit.Date = GetDateInput();
                        break;

                    case "t":
                        habit.Duration = GetDurationInput();
                        break;

                    case "s":
                        updating = false;
                        break;

                    case "0":                        
                        updating = false;
                        MainMenu();
                        break;

                    default:
                        Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");
                        break;
                }
            }

            codingController.Update(habit);
            MainMenu();

        }

        /// <summary>
        /// Method <c>ProcessReport</c> gets the user input for a selected month pf a chosen year and passes it to GetByMonthAndYear
        /// </summary>
        private void ProcessReport()
        {

            var yearAndMonth = GetMonthAndYearInput();
            
            codingController.GetByMonthAndYear(yearAndMonth);

        }

        
        /// <summary>
        /// Method <c>ProcessDelete</c> displays all records, gets user input for the record to be deleted and validates it
        /// </summary>
        private void ProcessDelete()
        {
            codingController.Get();
            Console.WriteLine("Please add id of the record you want to delete (or 0 to return to Main Menu");

            string commandInput = Console.ReadLine();
            
            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("\n You have to type a valide Id (or 0 to return to Main Menu). \n");
                commandInput = Console.ReadLine();  
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
                ProcessDelete();
            }

            codingController.Delete(id);
            MainMenu();

        }
             /// <summary>
             /// Method <c>GetHabitNameInput</c> gets the habit name input, validates it 
             /// and changes the name of the habit to a full word
             /// </summary>
             /// <returns>string</returns>             
        internal string GetHabitNameInput()
        {
            Console.WriteLine("\nWhich habit would you like to enter:");
            Console.WriteLine("Type 'c' for Coding");
            Console.WriteLine("Type 'd' for Distractions");
            Console.WriteLine("Type '0' to Go Back to the Main Menu");

            string habitNameInput = Console.ReadLine();

            if (habitNameInput == "0") MainMenu();

            string habitNameValidated = validation.HabitNameChoice(habitNameInput);

            if (habitNameValidated == "c")
            {
                habitNameValidated = "Coding";
            }
            else
            {
                habitNameValidated = "Distractions";
            }

            return habitNameValidated;
       
        }
        /// <summary>
        /// Method <c>GetDateInput</c> gets date input and validates it
        /// </summary>
        /// <returns>string</returns>
        internal string GetDateInput()
        {
            Console.WriteLine("\nPlease enter the date: (Format: dd-mm-yyyy). Type 0 to return to the Main Menu. \n\n");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") MainMenu();

            string dateInputValidated = validation.DateInput(dateInput);

            return dateInputValidated;
        }

        /// <summary>
        /// Method <c>GetDurationInput</c> gets duration input by offering a choice of entering different types of records,
        /// calculates duration that is then converted into a string.
        /// </summary>
        /// <returns>string</returns>
        internal string GetDurationInput()
        {
            
            var inputType = "";
            
           bool choosingInputType = true;
           while (choosingInputType == true)
           {
               Console.WriteLine($"\nSelect the type of record you want to enter: \n");
               Console.WriteLine("Type '1' for Duration");
               Console.WriteLine("Type '2' for Start & Finish times");
               Console.WriteLine("Type '3' for Pomodoro");
               Console.WriteLine("Type '4' for Real-time tracking");
               Console.WriteLine("Type '0' to Go Back to the Main Menu");

               inputType = Console.ReadLine();

               switch (inputType)
               {
                   case "0":
                       choosingInputType = false;
                       MainMenu();
                       break;

                   case "1":
                        choosingInputType = false;
                        inputType = GetDurationType();
                        break;

                   case "2":
                        choosingInputType = false;
                        inputType = GetStartAndFinishTimes();
                        break;

                   case "3":
                        choosingInputType = false;
                        inputType = GetPomodoros();
                        break;

                   case "4":
                        choosingInputType = false;
                        inputType = GetRealTimeTracking();
                        break;

                   default:
                       Console.WriteLine($"\nInvalid command. Please type a number from '0' to '4'.  Type '0' to Go Back to Main Menu\n");
                       break;
               }

           }
            return inputType;
                 
        }

        
        /// <summary>
        /// Method <c>GetDurationType</c> gets user duration input and validates it
        /// </summary>
        /// <returns>string</returns>
        internal string GetDurationType()
        {
            Console.WriteLine("\nPlease enter the duration: (Format: hh:mm). Type 0 to return to the main menu.");

            string durationInput = Console.ReadLine();

            if (durationInput == "0") MainMenu();

            string durationInputValidated = validation.DurationInput(durationInput);
                     
            return durationInputValidated;
        }

        /// <summary>
        /// Method <c>GetStartAndFinishTimes</c> gets start and finish times and validates them.
        /// </summary>
        /// <returns>string</returns>
        internal string GetStartAndFinishTimes() 
        {
            Console.WriteLine("\nPlease enter the Start Time (Format:hh:mm ). Type 0 to return to the Main Menu");
            string startTimeInput = Console.ReadLine();

            if (startTimeInput == "0") MainMenu();

            TimeOnly startTimeInputValidated = validation.Time(startTimeInput);

            Console.WriteLine("\nPlease enter the End Time (Format:hh:mm ). Type 0 to return to the Main Menu");
            string endTimeInput = Console.ReadLine();

            if (startTimeInput == "0") MainMenu();

            TimeOnly endTimeInputValidated = validation.Time(endTimeInput);

            string durationInput = durationCalculator.DurationTime(startTimeInputValidated, endTimeInputValidated);

            return durationInput;
        }

        /// <summary>
        /// Method <c>GetPomodoros</c> gets the number of pomodoros, validates the input and converts it into duration.
        /// </summary>
        /// <returns>string</returns>
        internal string GetPomodoros()
        {
            Console.WriteLine("\nPlease enter the number of pomodoros you would like to add");
            string pomodorosInput = Console.ReadLine();

            int pomodorosInputValidated = validation.Pomodoros(pomodorosInput);

            string pomodoroDuration = durationCalculator.PomodorosDuration(pomodorosInputValidated);

            return pomodoroDuration;
        }

        /// <summary>
        /// Method <c>GetRealTimeTracking</c> creates a stop watch and calculates duration
        /// </summary>
        /// <returns>string</returns>
        internal string GetRealTimeTracking()
        {
            Stopwatch stopWatch = new Stopwatch();
            Console.WriteLine("\nPlease type 's' to start the Stopwatch or press 0 to return to the Main Menu");
            string startInput = Console.ReadLine();

            if (startInput == "0") MainMenu();

            string startInputValidated = validation.StartStopwatch(startInput);

            stopWatch.Start();

            Console.WriteLine("\nPlease type 'e' to stop the Stopwatch or press 0 to return to the Main Menu.");
            string endInput = Console.ReadLine();

            if (endInput == "0") MainMenu();

            string endInputValidated = validation.EndStopwatch(endInput);

            stopWatch.Stop();
            
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}",    
            ts.Hours, ts.Minutes);
        
            return elapsedTime;
        }

        /// <summary>
        /// Method <c>GetMonthAndYear</c> gets user input for month and year, validates it
        /// </summary>
        /// <returns></returns>
        private DateTime GetMonthAndYearInput()
        {
            Console.WriteLine("Please type month (MM)");
            string monthInput = Console.ReadLine();

            int month = validation.ValidateMonth(monthInput);   

            Console.WriteLine("Please type year (a number from 2000 to 9999)");

            string yearInput = Console.ReadLine();

            int year = validation.ValidateYear(yearInput);


            var yearMonth = new DateTime(year, month, 01, 00, 00, 00);

            

            return yearMonth; 
            

           
        }




    }
}