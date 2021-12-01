using System;
using System.Security.Cryptography;
using System.Text;


namespace Hashed_Database_Users
{
    public class User
    {
        public string first_name;
        public string last_name;
        public string hashed_password = null;
        public string salt = null;
        public string email_add;

        #region ~ User : ctor
        public User(string fname, string lname, string pass, string sel, string mail)
        {
            this.first_name = fname;
            this.last_name = lname;
            this.hashed_password = pass;
            this.salt = sel;
            this.email_add = mail;
        }
        #endregion

        #region ~ public string : replaces user's input by stars
        public string passInput()
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
        public void defPass()
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
                    this.hashed_password = pass1;
                    break;
                }
            }
        }
        #endregion

        #region ~ void : sets the hashed password for a user
        public string hashPass()
        {            
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //randomly adds salt to a clear password
                this.salt = RandomString();
                this.hashed_password = this.hashed_password + this.salt;

                //Hash(password + salt)
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.hashed_password));

                StringBuilder builder = new StringBuilder();
                for (int k = 0; k < bytes.Length; k++)
                {
                    // "x2" returns the hexadecimal string
                    builder.Append(bytes[k].ToString("x2"));
                }
                this.hashed_password = builder.ToString();             
            }
            return this.hashed_password;
        }
        #endregion

        #region ~ string : returns a random salt
        public string RandomString()
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789&é(-_çà)=,;:/!";
            var builder = new StringBuilder("");

            for (int k = 0; k < 8; k++)
            {
                var rand = new Random();
                builder.Append( chars[ rand.Next(chars.Length) ] );
            }
            return builder.ToString();
        }
        #endregion

    }
}
