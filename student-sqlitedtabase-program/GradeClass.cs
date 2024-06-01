using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_sqlitedtabase_program
{
    internal class Grade
    {
        Student StudentName;
        public string stringGrade;
        public int SubjectId;
        public int StudentId;

        public Grade(string grade, Subject subjectName)
        {
            this.stringGrade = grade;
            SubjectId = subjectName.SubjectId;
            StudentId = subjectName.StudentId;
        }
        /*public static double CalculateGradeAvg(int subjectId, Student student)
        {
            double sumOfGrades = 0;
            subjectId -= 1;

            foreach (var grade in student.SubjectList[subjectId].GradeList) sumOfGrades += Convert.ToDouble(grade.stringGrade);
            double gradeAvg = sumOfGrades / student.SubjectList[subjectId].GradeList.Count;
            //Console.WriteLine($"{student.Name} has a grade average of {gradeAvg}, at {student.SubjectList[subjectId].SubjectName}");
            return gradeAvg;
        }*/
    }
}
