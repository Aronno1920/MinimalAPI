using StudentMinimalAPI.Models;

namespace StudentMinimalAPI.Data
{
    public static class StudentStore
    {
        public static List<Student> StudentList = new List<Student> {
            new Student { Id = 1, Name = "Selim Ahmed", Email = "selim@email.com", MobileNo = "01711-123123" },
            new Student { Id = 2, Name = "Afsara Tasnim", Email = "afsara@email.com", MobileNo = "01911-123123" },
            new Student { Id = 3, Name = "Sanaya Ahmed", Email = "sanaya@email.com", MobileNo = "01811-123123" },
            new Student { Id = 4, Name = "Aairah", Email = "aairah@email.com", MobileNo = "01511-123123" }
        };
    }
}
