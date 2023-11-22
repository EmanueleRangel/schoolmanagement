using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly IStudentRepository _studentRepository;

    public StudentController(IApplicationDbContext context,
    IStudentRepository studentRepository)
    {
        _context = context;
        _studentRepository = studentRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Student>> Get()
    {
        return await  _studentRepository.GetAllStudents();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var aluno = _studentRepository.GetStudentById(id);
        if (aluno == null)
        {
            return NotFound();
        }
        return Ok(aluno);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Student student)
    {
        _studentRepository.AddStudent(student);
        _context.SaveChanges();
         try
    {
        await _context.SaveChangesAsync();
        return Ok(student);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Internal server error");
    }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Student aluno)
    {
        if (id != aluno.Id)
        {
            return BadRequest();
        }

        _studentRepository.UpdateStudent(aluno);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var aluno = _context.Students.Find(id);
        if (aluno == null)
        {
            return NotFound();
        }

        _studentRepository.DeleteStudent(id);
        _context.SaveChanges();
        return NoContent();
    }
}
