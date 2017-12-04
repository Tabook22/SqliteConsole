
using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace SimpleSqlite
{
    public class myServer
    {
        // string location = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //string fileName = "myDB.db";
        //string fullPath = Path.Combine(location, fileName);
        public string connectionString = string.Format("Data Source= {0}", "myDB.db");

        //Create New Database if it not exists
        public void createDataBase()
        {
            if (!dublicateDataBase("myDB.db"))
            {
                string createTable = "CREATE TABLE 'userLoginTable' ('id' INTEGER PRIMARY KEY AUTOINCREMENT,'username' TEXT);";
                using (SqliteConnection SqlConn = new SqliteConnection(connectionString))
                {
                    SqliteCommand cmd = new SqliteCommand(createTable, SqlConn);
                    SqlConn.Open();
                    cmd.ExecuteNonQuery();
                    SqlConn.Close();
                }
                Console.WriteLine("Database Created !!");

            }
        }

        // Add New User
        public void insertUserName(string usr)
        {
            using (SqliteConnection SqlConn = new SqliteConnection(connectionString))
            {
                if (SqlConn.State.ToString() != "Open")
                {
                    SqlConn.Open();
                    string insertUserQuery = "INSERT INTO userLoginTable(username) VALUES(@user)";
                   
                    SqliteCommand cmd = new SqliteCommand(insertUserQuery, SqlConn);
                    cmd.Parameters.AddWithValue("@user", usr);

                    //cmd.Parameters.AddWithValue("@user", username);
                    cmd.ExecuteNonQuery();   
                }
                SqlConn.Close();
            }
            Console.WriteLine("One Record is Created");
        }

        //Insert multiple users
        public void insertMulitpleUsers(string[] usr)
        {
            using (SqliteConnection SqlConn = new SqliteConnection(connectionString))
            {
                if (SqlConn.State.ToString() != "Open")
                {
                    SqlConn.Open();
                    string insertUserQuery = "INSERT INTO userLoginTable(username) VALUES(@user)";
                    for(int i=0;i<usr.Length; ++i){
                                         SqliteCommand cmd = new SqliteCommand(insertUserQuery, SqlConn);
                    cmd.Parameters.AddWithValue("@user", usr[i]);

                    //cmd.Parameters.AddWithValue("@user", username);
                    cmd.ExecuteNonQuery();   
                    Console.WriteLine("Record {0} is Created", i); 
                    }


                }
                SqlConn.Close();
            }
           // Console.WriteLine("One Record is Created");
        }


        //Remove user
        public void removeUserName(string username, int id)
        {
            using (SqliteConnection SqlConn = new SqliteConnection(connectionString))
            {
                string removeUserNameQuery = "DELETE FROM userLoginTable WHERE username=@user AND @id";
                SqliteCommand cmd = new SqliteCommand(removeUserNameQuery, SqlConn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@id", id);
                SqlConn.Open();
                cmd.ExecuteNonQuery();
                SqlConn.Close();
            }
            Console.WriteLine("One Record is Deleted");
        }

        // Update users
        public void updateUserName(string username, int id)
        {
            using (SqliteConnection SqlConn = new SqliteConnection(connectionString))
            {
                string updateUserNameQuery = "UPDATE userLoginTable SET username=@newValue WHERE username=@user AND id=@id";
                SqliteCommand cmd = new SqliteCommand(updateUserNameQuery, SqlConn);
                cmd.Parameters.AddWithValue("@newValue", "NasserT.");
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@id", id);
                SqlConn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("One Record is Updated");
        }

        //Get all users
        public void readUserNames()
        {
            using (SqliteConnection SqlConn = new SqliteConnection(connectionString))
            {
                string readUserNamesQuery = "SELECT * FROM userLoginTable";
                SqliteCommand cmd = new SqliteCommand(readUserNamesQuery, SqlConn);
                SqlConn.Open();
                SqliteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        Console.WriteLine(reader.GetValue(i));
                    }
                }
            }
        }



        //Check if Database Exists
        private bool dublicateDataBase(string fullPath)
        {
            return System.IO.File.Exists(fullPath);
        }

    }
}
