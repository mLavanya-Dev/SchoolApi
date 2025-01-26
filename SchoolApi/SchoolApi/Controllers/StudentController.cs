using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        public StudentController(IStudentService studentService,ICourseService courseService) 
        {
            _studentService = studentService;
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()=>Ok(await _studentService.GetAll());
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Student>> GetStudentById(string id)
        {
            var student=await _studentService.GetById(id);
            if(student == null)
            {
                return NotFound();
            }
            if(student.Courses is null || !student.Courses.Any())
            {
                return Ok(student);
            }
            student.CourseList = new List<Course>();
            foreach(var courseId in student.Courses)
            {
                var course = await _courseService.GetById(courseId) ?? throw new Exception("Invalid Course Id");
                student.CourseList.Add(course);
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var createdStudent=await _studentService.Create(student);
            return createdStudent is null?throw new Exception("student creation failed"): CreatedAtAction(nameof(GetStudentById),new {id=createdStudent.Id},student);
        }
        [HttpPut/*("{id:length(24)}")*/]
        public async Task<IActionResult> Update(string id,Student student)
        {
            var queriedStudent=await _studentService.GetById(id);
            if(queriedStudent is null)
            {
                return NotFound();
            }
            await _studentService.Update(id,student);
            return NoContent();

        }
        [HttpDelete/*("{id:length(24}")*/]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetById(id);
            if (student is null)
            {
                return NotFound();
            }
            await _studentService.Delete(id);
            return NoContent();
        }
    }
}
