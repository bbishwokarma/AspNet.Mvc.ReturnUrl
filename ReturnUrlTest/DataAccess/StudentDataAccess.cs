using ReturnUrlTest.Models;
using System.Collections.Generic;

namespace ReturnUrlTest.DataAccess
{
    public class StudentDataAccess
    {
        private static List<Student> _students = new List<Student>();

        static StudentDataAccess()
        {
            _students.Add(new Student() { StudentId = "S001", FirstName = "John", LastName = "Paul", EnrollmentDate = new System.DateTime(2015, 10, 15) });
            _students.Add(new Student() { StudentId = "S002", FirstName = "John", LastName = "Smith", EnrollmentDate = new System.DateTime(2015, 10, 15) });
            _students.Add(new Student() { StudentId = "S003", FirstName = "Phillip", LastName = "Paul", EnrollmentDate = new System.DateTime(2015, 10, 15) });
            _students.Add(new Student() { StudentId = "S004", FirstName = "Johnson", LastName = "Paul", EnrollmentDate = new System.DateTime(2016, 10, 15) });
            _students.Add(new Student() { StudentId = "S005", FirstName = "Rickey", LastName = "Paul", EnrollmentDate = new System.DateTime(2016, 10, 15) });
            _students.Add(new Student() { StudentId = "S006", FirstName = "John", LastName = "Steward", EnrollmentDate = new System.DateTime(2016, 10, 15) });
            _students.Add(new Student() { StudentId = "S007", FirstName = "Mark", LastName = "Paul", EnrollmentDate = new System.DateTime(2016, 10, 15) });
            _students.Add(new Student() { StudentId = "S008", FirstName = "Mitchell", LastName = "Paul", EnrollmentDate = new System.DateTime(2017, 1, 15) });
            _students.Add(new Student() { StudentId = "S009", FirstName = "Melissa", LastName = "Paul", EnrollmentDate = new System.DateTime(2017, 1, 15) });
            _students.Add(new Student() { StudentId = "S010", FirstName = "Yun", LastName = "Zhung", EnrollmentDate = new System.DateTime(2017, 1, 15) });
        }

        public List<Student> Students { get { return _students; } }
    }
}