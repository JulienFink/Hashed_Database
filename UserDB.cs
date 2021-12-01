using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Hashed_Database_Users
{
    class UserDB
    {
        public MySqlConnection connection = null;

        #region Ctor
        public UserDB()
        {
            try
            {
                string connexionString = "SERVER=****************;PORT=****************;" +
                                        "DATABASE=****************;" +
                                        "UID=****************;PASSWORD=****************";

                this.connection = new MySqlConnection(connexionString);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" Connection Error : " + e.ToString());
                return;
            }
        }
        #endregion

        #region ~ void : displays message either authentication is successful or not
        private void authType(int number)
        {
            Console.WriteLine();
            if (number == 0) { Console.WriteLine("Login successful !"); }
            if (number == 1) { Console.WriteLine("Authentication failed."); }
            Console.WriteLine();
        }
        #endregion

        #region ~ void : defines a new User
        public void defUser()
        {
            Console.Write("Enter your first name : ");
            string fname = Console.ReadLine();

            Console.Write("Enter your last name : ");
            string lname = Console.ReadLine();

            Console.Write("Enter your email : ");
            string email = Console.ReadLine();

            User k = new User(fname, lname, null, null, email);
            //Defines password for the user
            k.defPass();

            k.hashPass();

            //Creates a new User in the database
            createUser(k);
        }
        #endregion

        #region ~ void : creates a new user in the database
        public void createUser(User k)
        {
            
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " INSERT INTO users (id, first_name, last_name, password, salt, mail) "
                                + $"VALUES(NULL, '{k.first_name}', '{k.last_name}', '{k.hashed_password}', '{k.salt}', '{k.email_add}');";
            command.ExecuteReader();          
        }
        #endregion

        #region ~ bool : checks if a (user) mail is in DB
        public bool checksUser(string mail)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT COUNT(*) FROM users "
                                    + $"WHERE mail = '{mail}';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();

                int value = 0;
                while(reader.Read())
                {
                    value = reader.GetInt32(0);
                    if(value == 1)
                    {
                        connection.Close();
                        return true;
                    }                   
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Error.");
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        #endregion

        #region ~ bool : login - first part
        public void init_login()
        {
            Console.Write("Enter your email : ");
            string mail = Console.ReadLine();

            Console.Write("Enter your password : ");
            User dummy = new User(null, null, null, null, null);
            string password = dummy.passInput();

            Console.WriteLine();
            if (checksUser(mail))
            {
                loginUser(mail, password);
            }
            else
            {
                authType(1);
            }
        }
        #endregion

        #region ~ void : login - second part
        public void loginUser(string mail, string password)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT password, salt FROM users "
                                    + $"WHERE mail = '{mail}';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string hashed_password1 = reader.GetString(0);
                    string salt1 = reader.GetString(1);

                    string hashed = hash(password, salt1);

                    if (hashed_password1 == hashed)
                    {
                        authType(0);
                    }
                    else
                    {
                        authType(1);
                    }
                }
            }              
            catch (MySqlException)
            {
                Console.WriteLine("Error.");
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region ~ void : computes the hash from
        public string hash(string password, string salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string to_be_hashed = password + salt;

                //Hash(password + salt)
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(to_be_hashed));

                StringBuilder builder = new StringBuilder();
                for (int k = 0; k < bytes.Length; k++)
                {
                    // "x2" returns the hexadecimal string
                    builder.Append(bytes[k].ToString("x2"));
                }
                string hashed = builder.ToString();
                return hashed;
            }            
        }
        #endregion

    }
}
