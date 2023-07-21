using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;

namespace SipayApi.Controllers;



public class Student
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

[NonController]
[ApiController]
[Route("sipy/api/[controller]")]
public class StudentController : ControllerBase
{
    private List<Student> list;
    public StudentController()
    {
        list = new();
        list.Add(new Student { Id = 1, Age = 23, Email = "deny@sellen.com", Lastname = "Sellen", Name = "Deny" });
        list.Add(new Student { Id = 2, Age = 24, Email = "deny@sellen.com", Lastname = "Solomor", Name = "Henry" });
        list.Add(new Student { Id = 3, Age = 25, Email = "deny@sellen.com", Lastname = "Felix", Name = "Jeff" });
    }


    [HttpGet]
    public ApiResponse<List<Student>> Get()
    {
        return new ApiResponse<List<Student>>(list);
    }

    [HttpGet("{id}")]
    public ApiResponse<Student> Get(int id)
    {
        return new ApiResponse<Student>(list.FirstOrDefault(x => x.Id == id));
    }

    [HttpGet("ByParameters")]
    public ApiResponse<List<Student>> Get([FromQuery] string? name, [FromQuery] string? lastname, [FromQuery] int? age)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            list = list.Where(x=> x.Name.ToUpper().Contains(name.ToUpper())).ToList();
        }
        if (!string.IsNullOrWhiteSpace(lastname))
        {
            list = list.Where(x => x.Lastname.ToUpper().Contains(lastname.ToUpper())).ToList();
        }
        if (age> 0)
        {
            list = list.Where(x => x.Age >= age).ToList();
        }
        return new ApiResponse<List<Student>>(list);
    }

    [HttpPost]
    public ApiResponse<List<Student>> Post([FromBody] Student student)
    {
        student.Id = list.Count() + 1;
        list.Add(student);
        return new ApiResponse<List<Student>>(list);
    }

    [HttpPut("{id}")]
    public ApiResponse<List<Student>> Put(int id, [FromBody] Student student)
    {
        var exist = list.FirstOrDefault(x => x.Id == id);   
        if (exist != null)
        {
            list.Remove(exist);
            student.Id = id;
            list.Add(student);
        }
        return new ApiResponse<List<Student>>(list);
    }


    [HttpDelete("{id}")]
    public ApiResponse<List<Student>> Delete(int id)
    {
        var exist = list.FirstOrDefault(x => x.Id == id);
        if (exist != null)
        {
            list.Remove(exist);           
        }
        return new ApiResponse<List<Student>>(list);
    }
}
