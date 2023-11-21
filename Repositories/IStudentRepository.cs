public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllStudents();
    Task<Student> GetStudentById(int studentId);
    Task AddStudent(Student student);
    Task UpdateStudent(Student student);
    Task DeleteStudent(int studentId);
}
