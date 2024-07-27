using Microsoft.AspNetCore.Mvc;
using MyStudentWebAPI.DataSimulation;
using MyStudentWebAPI.Model;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MyStudentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {



        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if(StudentData.StudentsList.Count == 0)
            {
                return NotFound("No students found.");
            }
            return Ok(StudentData.StudentsList);
        }



        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var PassedStudent = StudentData.StudentsList.Where(student => student.Grade >= 50).ToList();
         
            if (PassedStudent.Count == 0)
            {
                return NotFound("No Passed students found.");
            }

            return Ok(PassedStudent);
            
        }





        [HttpGet("AverageGrades", Name = "GetAverageGrades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAverageGrades()
        {
            if (StudentData.StudentsList.Count == 0)
                return NotFound("No students found!");

            var avgGrades = StudentData.StudentsList.Average(student => student.Grade);
            return Ok(avgGrades);
        }
      
        
        
        
        [HttpGet("{StudentID}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetStudentById(int StudentID)
        {
            if (StudentID < 1)
            {
                return BadRequest("Not accepted ID");
            }

            var Student = StudentData.StudentsList.FirstOrDefault(student => student.Id == StudentID);

            if (Student == null)
            {
                return NotFound($"Student with id {StudentID} is not found!");
            }

            return Ok (Student);
        }



        [HttpPost(Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> AddNewStudent(Student NewStudent)
        {
            if (NewStudent == null || String.IsNullOrEmpty(NewStudent.Name) || NewStudent.Age <= 0)
            {
                return BadRequest("Invalid student data.");
            }

            NewStudent.Id = StudentData.StudentsList.Count > 0 ? StudentData.StudentsList.Max(student => student.Id) + 1 : 1;

            StudentData.StudentsList.Add(NewStudent);

            return CreatedAtRoute("GetStudentById", new { StudentID = NewStudent.Id }, NewStudent);
        }




        [HttpDelete(Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int StudentID)
        {
            if (StudentID <= 0)
            {
                return BadRequest($"Not accepted ID {StudentID}");
            }

            var Student = StudentData.StudentsList.FirstOrDefault(s => s.Id == StudentID);

            if (Student == null)
                return NotFound($"Student with ID {StudentID} was not found.");

            StudentData.StudentsList.Remove(Student);

            return Ok($"Student with ID {StudentID} has been deleted succefully.");
        }




        [HttpPut("{StudentID}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> UpdateStudent(int StudentID, Student updatedStudent)
        {
            if (StudentID < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 )
            {
                return BadRequest("Invalid student data.");
            }

            Student student = StudentData.StudentsList.FirstOrDefault(s => s.Id == StudentID);
            if (student == null)
            {
                return NotFound($"Student with ID {StudentID} not found.");
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;

            return Ok(student);
        }

    }
}
