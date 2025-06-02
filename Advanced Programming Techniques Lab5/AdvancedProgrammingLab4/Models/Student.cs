using System; // Add this using directive

namespace AdvancedProgrammingLab4.Models
{
    [Serializable] // Add this attribute
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}