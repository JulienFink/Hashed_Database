using System;

namespace Hashed_Database_Users
{
    class Program
    {
        static void welcome(UserDB x)
        {
            Console.WriteLine();
            Console.WriteLine("- - - - - - - - - - - - - - - - - - -");
            Console.WriteLine("Welcome ! What would you like to do ?");
            Console.WriteLine();
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Leave");
            Console.WriteLine();
            Console.WriteLine("Choose one option among those above : ");
            string choice = Console.ReadLine();

            if(choice == "1")
            {
                x.defUser();
                Environment.Exit(0);
            }
            if(choice == "2")
            {
                x.init_login();
                Environment.Exit(0);
            }
            if(choice == "3")
            {
                Environment.Exit(0);
            }            
        }

        static void Main(string[] args)
        {
            UserDB db = new UserDB();
            welcome(db);         
        }
    }
}
