using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace EP.Balda.Data
{
    class Transaction
    {
        public void Do()
        {
            var con = new SQLiteConnection(@"Data Source =..\EP.Balda.Data\DbStore\baldaGameDb.db;Version=3;New=True;Compress=True;");
            
            try
            {
                con.Open();
                int counter = 0;
                string line;

                // Read the file and display it line by line.  
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"c:\test.txt");
                while ((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    counter++;
                }

                file.Close();
                System.Console.WriteLine("There were {0} lines.", counter);
                // Suspend the screen.  
                System.Console.ReadLine();
                string Query = "insert into sqliteDb(ID,Name) values('"+ "')";
            
                SQLiteCommand cmd = new SQLiteCommand(Query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
            
            
            
        }
    }
}
