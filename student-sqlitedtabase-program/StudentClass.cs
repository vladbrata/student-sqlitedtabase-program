using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace student_sqlitedtabase_program
{
    internal class Student
    {
        public string Name;
        public int StudentId;
        public int ClassId;
        public Clasa ClassName;
        public List<Grade> GradeList = new List<Grade>();
        public List<Subject> SubjectList = new List<Subject>();
        public Student(string name, Clasa className)
        {
            this.Name = name;
            this.ClassName = className;
            //ClassName.NumberOfStudents++;

            ClassId = className.ClassId;
            StudentId = GetStudentId();
        }

        private int GetStudentId()
        {
            string connectionString = "Data Source=students-database.db;Version=3;";
            int id = 0;

            Student student = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT StudentId FROM Students";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["StudentId"]);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }
            return id;
        }
        public void PrintSubjects()
        {
            foreach (Subject subject in SubjectList)
            {
                Console.Write($"{subject.SubjectName} (subject id: {subject.SubjectId}): ");
                subject.PrintGrades();
            }
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Informatii pentru elevul {Name}:");
            Console.WriteLine($"Este in clasa {ClassName.Name}");
            Console.WriteLine($"{Name} urmeaza cursuri urmatoarele materii, el avand notele afisate: ");
            PrintSubjects();

        }

    }
}
