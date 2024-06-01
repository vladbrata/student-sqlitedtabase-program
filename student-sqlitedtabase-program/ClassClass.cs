using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_sqlitedtabase_program
{
    internal class Clasa
    {
        public string Name;
        public int ClassId;
        public int NumberOfStudents;
        public static List<Clasa> ClassList = new List<Clasa>();
        public List<Student> StudentList = new List<Student>();
        public Dictionary<int, Student> ListOfStudents = new Dictionary<int, Student>();

        public Clasa(string name)
        {
            this.Name = name;
            ClassId = GetClassId();
        }

        public void PrintStudents()
        {
            foreach (var student in StudentList) Console.Write($"{student.Name}({student.StudentId}) \n");
        }
        private int GetClassId()
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            int id = 0;


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT ClassID FROM Classes";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["ClassId"]);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }
            return id;
        }
        /*public static void PrintClasses(List<Clasa> classList)
        {

            foreach (var clasa in classList) Console.WriteLine($"{clasa.Name} ({clasa.ClassId})");
        }*/
    }
}
