namespace coding_time_tracker
{
    internal class GetUserInput
    {
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
                Console.WriteLine("--------------------------------------\n");

                string userMenuChoice = Console.ReadLine();


                while (string.IsNullOrEmpty(userMenuChoice))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4. \n");
                    userMenuChoice = Console.ReadLine();

                }


                switch (userMenuChoice)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        // codingController.Get();
                        break;

                    case "2":
                        // ProcessAdd();
                        break;

                    case "3":
                        //  ProcessUpdate();
                        break;

                    case "4":
                        //   ProcessDelete();
                        break;

                    case "default":
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4. \n");
                        break;

                }

            }
        }
    }
}