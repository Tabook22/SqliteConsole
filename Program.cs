using System;
using SimpleSqlite;

namespace SimpleSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
          myServer server=new myServer();
          server.createDataBase();
          string[] users=new string[]{"Nasser","Salim","Ahmed"};
          server.insertUserName("Nasser Tabook");  
          server.insertMulitpleUsers(users);  
          server.updateUserName("Nasser Tabook", 14); 
          server.readUserNames();  
        }
    }
}
