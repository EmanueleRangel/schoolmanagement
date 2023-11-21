using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> Get()
    {
        return _context.Students.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Student> Get(int id)
    {
        var aluno = _context.Students.Find(id);
        if (aluno == null)
        {
            return NotFound();
        }
        return aluno;
    }

    [HttpPost]
    public ActionResult<Student> Post([FromBody] Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Student aluno)
    {
        if (id != aluno.Id)
        {
            return BadRequest();
        }

        _context.Entry(aluno).State = EntityState.Modified;
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

        _context.Students.Remove(aluno);
        _context.SaveChanges();
        return NoContent();
    }
}
