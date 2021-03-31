using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace Hashed_Database_Users
{
    public class User
    {
        //Increment
        //private static int count = 0;

        //Class attributes
        //public int id;

        public string first_name;
        public string last_name;
        public string hashed_password = null;
        public string email_add;

        #region ~ User : ctor
        public User(string fname, string lname, string mail)
        {
            //this.id = System.Threading.Interlocked.Increment(ref count); Auto increments 'id' at each class initialization
            this.first_name = fname;
            this.last_name = lname;
            this.email_add = mail;            
        }
        #endregion

        #region ~ private string : replaces user's input by stars
        private string passInput()
        {
            var password = string.Empty;
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        return null;
                    case ConsoleKey.Enter:
                        return password;
                    case ConsoleKey.Backspace:
                        if (password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        break;
                    default:
                        password += key.KeyChar;
                        Console.Write("*");
                        break;
                }
            }
        }
        #endregion

        #region ~ private string : defines a new password for a user (checks length and equality)
        private string defPass()
        {
            while (true)
            {
                var pass1 = string.Empty;
                var pass2 = string.Empty;

                Console.Write("Enter your new password : ");
                pass1 = passInput();
                Console.WriteLine();

                Console.Write("Enter your password again : ");
                pass2 = passInput();
                Console.WriteLine();

                if (pass1.Length < 8)
                {
                    Console.WriteLine("Password must be at least 8 characters long...");
                    Console.WriteLine();
                }
                if (pass1 != pass2)
                {
                    Console.WriteLine("Both passwords must be equal...");
                    Console.WriteLine();
                }
                else
                {
                    return pass1;
                }
            }
        }
        #endregion

        #region ~ void : sets the hashed password for a user
        public string hashPass()
        {
            int i = 0;
            string data = "";
            if(this.hashed_password == null)
            {
                data = defPass();
                i++;
            }
            else
            {
                data = passInput();
            }
            
            using (SHA256 sha256Hash = SHA256.Create())
            { 
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder builder = new StringBuilder();
                for (int k = 0; k < bytes.Length; k++)
                {
                    // "x2" returns the hexadecimal string
                    builder.Append(bytes[k].ToString("x2"));
                }
                this.hashed_password = builder.ToString();

                if(i == 1) { addSaltAndPepper(); }               
            }
            return this.hashed_password;
        }
        #endregion

        #region ~ private void : adds salt and pepper to the hashed password
        private void addSaltAndPepper()
        {
            //Randomly assign salt and pepper to the hashed password in DB
            List<string> seasoning = new List<string>();
            seasoning.Add("168fa43b");
            seasoning.Add("bc83173d");
            seasoning.Add("667ffyy3");
            seasoning.Add("21c4b90w");
            seasoning.Add("f2275xb2");

            Random s = new Random();
            int salt = s.Next(0, 8);

            Random p = new Random();
            int pepper = p.Next(0, 8);

            this.hashed_password = seasoning[salt] + this.hashed_password + seasoning[pepper];
        }
        #endregion

        #region ~ bool : verify the hash vs. a string
        public bool verifyHash(string hash)
        {
            //Remove first and last five characters to read the real passord
            string cut = this.hashed_password.Substring(5, this.hashed_password.Length - 10);

            return hash == cut;
        }
        #endregion

    }
}
