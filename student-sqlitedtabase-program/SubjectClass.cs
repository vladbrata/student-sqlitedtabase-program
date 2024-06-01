using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_sqlitedtabase_program
{
    internal class Subject
    {
        public string SubjectName;
        public int StudentId;
        public int SubjectId;
        public Student StudentName;
        public List<Grade> GradeList = new List<Grade>();

        public Subject(string subjectName, Student studentName)
        {
            this.SubjectName = subjectName;
            this.StudentName = studentName;
            StudentId = studentName.StudentId;
            SubjectId = GetSubjectId();
        }

        private int GetSubjectId()
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            int id = 0;


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT SubjectId FROM Subjects";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["SubjectId"]);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }
            return id;
        }
        public void PrintGrades()
        {
            /*string output = "";
            foreach (Grade grade in GradeList)
            {
                output += (grade.stringGrade + " ");
            }
            Console.WriteLine($"{output} (grade average: {gradeAvg})");*/
            double gradeAvg = 0;
            foreach (Grade grade in GradeList)
            {
                gradeAvg = CalcGradeAvg(GradeList);
                Console.Write(grade.stringGrade + " ");
            }
            Console.WriteLine($"(grade average: {gradeAvg})");
        }
        public double CalcGradeAvg(List<Grade> GradeList)
        {
            double gradeAvg = 0;
            foreach (Grade grade in GradeList) gradeAvg += Convert.ToDouble(grade.stringGrade);
            gradeAvg /= GradeList.Count;
            return gradeAvg;
        }
    }
}
