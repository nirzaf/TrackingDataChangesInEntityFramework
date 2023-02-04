using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    public async Task<IActionResult> UpdateStudent([FromBody]Student student)
    {
        var studentToUpdate = await _context.Students.FindAsync(student.Id);
        if (studentToUpdate == null)
        {
            return NotFound();
        }
        //Update student in database
        if(!student.Name.Trim().IsNullOrEmpty()) studentToUpdate.Name = student.Name;
        if(!student.Email.Trim().IsNullOrEmpty()) studentToUpdate.Email = student.Email;
        if(!student.Phone.Trim().IsNullOrEmpty()) studentToUpdate.Phone = student.Phone;
        if(!student.Address.Trim().IsNullOrEmpty()) studentToUpdate.Address = student.Address;
        if(!student.City.Trim().IsNullOrEmpty()) studentToUpdate.City = student.City;
        if(!student.State.Trim().IsNullOrEmpty()) studentToUpdate.State = student.State;
        if(!student.Zip.Trim().IsNullOrEmpty()) studentToUpdate.Zip = student.Zip;
        
        _context.Students.Update(studentToUpdate);
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