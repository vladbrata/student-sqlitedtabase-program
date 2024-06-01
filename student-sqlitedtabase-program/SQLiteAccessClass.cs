using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static student_sqlitedtabase_program.Clasa;

namespace student_sqlitedtabase_program
{
    internal class SQLiteAccess
    {
        /*public static void AddClass(Clasa className)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                string insertQuery = "INSERT INTO Classes (ClassName) VALUES (@ClassName);";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", className.Name);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public static void AddStudent(Student studentName)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Students (StudentName, ClassId) VALUES (@StudentName, @ClassId);";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentName", studentName.Name);
                    command.Parameters.AddWithValue("@ClassId", studentName.ClassId);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public static void AddSubject(Subject subject)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Subjects (SubjectName, StudentId) VALUES (@SubjectName, @StudentId);";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                    command.Parameters.AddWithValue("@StudentId", subject.StudentId);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public static void AddGrade(Grade grade)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Grades (SubjectId, StudentId, Grade) VALUES (@SubjectId, @StudentId, @Grade);";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@SubjectId", grade.SubjectId);
                    command.Parameters.AddWithValue("@StudentId", grade.StudentId);
                    command.Parameters.AddWithValue("@Grade", grade.stringGrade);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }*/
        public static List<Clasa> GetClasses()
        {
            string connectionString = "Data Source=database-students.db;Version=3;";
            List<Clasa> clases = new List<Clasa>();
            Clasa clasa = null;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT ClassId, ClassName FROM Classes";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Read the data and populate the list
                        while (reader.Read())
                        {
                            string className = reader["ClassName"].ToString();
                            clasa = new Clasa(className);
                            clasa.ClassId = Convert.ToInt32(reader["ClassId"]);
                            clases.Add(clasa);

                            //clasa.ClassList.Add(clasa);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }
            return clases;
        }
        public static void GetSubjects(Student studentName)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";

            Subject subject = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT SubjectId, SubjectName, StudentId FROM Subjects";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Read the data and populate the list
                        while (reader.Read())
                        {
                            string subjectName = reader["SubjectName"].ToString();
                            subject = new Subject(subjectName, studentName)
                            {
                                SubjectId = Convert.ToInt32(reader["SubjectId"]),
                                StudentId = Convert.ToInt32(reader["StudentId"])
                            };
                            if (subject.StudentId == studentName.StudentId)
                                studentName.ListOfSubjects.Add(subject.SubjectId, subject);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }
        }
        private static void GetGrades(Subject subjectName)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";


            Grade grade = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT SubjectId, StudentId, Grade FROM Grades";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Read the data and populate the list
                        while (reader.Read())
                        {
                            string gradeVal = reader["Grade"].ToString();
                            grade = new Grade(gradeVal, subjectName)
                            {
                                SubjectId = Convert.ToInt32(reader["SubjectId"]),
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                stringGrade = Convert.ToString(reader["Grade"])
                            };
                            if (subjectName.StudentId == grade.StudentId && subjectName.SubjectId == grade.SubjectId)
                                subjectName.GradeList.Add(grade);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }

        }
        public static void GetGrades(Student studentName)
        {
            for (int i = 0; i < studentName.SubjectList.Count; i++)
            {
                SQLiteAccess.GetGrades(studentName.SubjectList[i]);

            }
        }
        public static void GetStudents(Clasa className)
        {
            string connectionString = "Data Source=database-students.db;Version=3;";


            Student student = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query to select all people from the table
                string selectDataQuery = "SELECT StudentId, StudentName, ClassId FROM Students";

                // Execute the query
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Read the data and populate the list
                        while (reader.Read())
                        {
                            string studentName = reader["StudentName"].ToString();
                            student = new Student(studentName, className)
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                ClassId = Convert.ToInt32(reader["ClassId"])
                            };
                            if (student.ClassId == className.ClassId)
                                className.StudentList.Add(student);
                        }
                    }
                }

                // Close the connection
                connection.Close();
            }
        }

    }
}
