using System.Collections.Generic;
using MyStudentWebAPI.Model;

namespace MyStudentWebAPI.DataSimulation
{
    public static class StudentData
    {
        public static readonly List<Student> StudentsList = new List<Student>
        {
            new Student { Id = 3, Name = "Hameedo Refai", Age = 21, Grade = 80 },
            new Student { Id = 1, Name = "John Doe", Age = 20, Grade = 79 },
            new Student { Id = 2, Name = "Jane Smith", Age = 22, Grade = 61 },
            new Student { Id = 4, Name = "Omair Khattab", Age = 22, Grade = 97 },
            new Student { Id = 5, Name = "Musailamah Kazzab", Age = 22, Grade = 35 }
        };

        public static string GetName ()
        {
            return "test name";
        }
    }
}
