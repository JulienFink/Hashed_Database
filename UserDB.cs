using MySql.Data.MySqlClient;
using System;

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
            if (number == 1) { Console.WriteLine("Authentication failed"); }
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

            User k = new User(fname, lname, email);
            //Defines password for the user
            k.hashPass();

            //Creates a new User in the database
            createUser(k);
        }
        #endregion

        #region ~ void : creates a new user in the database
        public void createUser(User k)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " INSERT INTO users (id, first_name, last_name, password, mail) "
                                    + $"VALUES(NULL, '{k.first_name}', '{k.last_name}', '{k.hashed_password}', '{k.email_add}');";
                command.ExecuteReader();
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
        public void login()
        {
            Console.Write("Enter your email : ");
            string mail = Console.ReadLine();

            Console.Write("Enter your password : ");
            User y = new User(null, null, null);
            y.hashed_password = "";
            string hash = y.hashPass();

            Console.WriteLine();

            if (checksUser(mail))
            {
                loginUser(mail, hash.ToString());
            }
            else
            {
                authType(1);
            }
        }
        #endregion

        #region ~ bool : login - second part
        public bool loginUser(string mail, string hash)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT * FROM users "
                                    + $"WHERE mail = '{mail}';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();

                User z = new User(null, null, null);

                while(reader.Read())
                {
                    z.first_name = reader.GetString(1);
                    z.last_name = reader.GetString(2);
                    z.hashed_password = reader.GetString(3);
                    z.email_add = reader.GetString(4);

                    if (z.verifyHash(hash) && z.email_add == mail)
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
            return false;
        }
        #endregion

    }
}
