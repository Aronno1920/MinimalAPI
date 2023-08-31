namespace StudentMinimalAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String RegNo { get; set; }
        public String MobileNo { get; set; }
        public String Email { get; set; }
        public DateTime? AdmissionDate { get; set; } = DateTime.Now;   
    }
}
