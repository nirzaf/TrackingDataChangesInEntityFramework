using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrackingDataChangesInEntityFramework.Controllers;

/*
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
 */

public class StudentController : Controller
{
    private readonly StudentContext _context;
    public StudentController(StudentContext context)
    {
        _context = context;
    }
    
    //get all students as list
    [HttpGet("api/students")]
    public async Task<List<Student>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }
    
    //fetch student by id
    [HttpGet("{id}")]
    public async Task<Student> GetStudent(int id)
    {
        //Get student from database
        var student = await _context.Students.FindAsync(id);
        return student;
    }
    
    [HttpPost("api/students")]
    public async Task<IActionResult> CreateStudent(InsertStudent student)
    {
        //Create student in database
        var newStudent = new Student
        {
            Name = student.Name,
            Email = student.Email,
            Phone = student.Phone,
            Address = student.Address,
            City = student.City,
            State = student.State,
            Zip = student.Zip
        };
        _context.Students.Add(newStudent);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut("api/students")]
    public async Task<IActionResult> UpdateStudent(Student student)
    {
        //Update student in database
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("api/students/{id}")]
    public async Task<IActionResult> DeleteStudent(Student student)
    {
        //Delete student from database
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return Ok();
    }

}