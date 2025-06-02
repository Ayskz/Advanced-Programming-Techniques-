namespace AdvancedProgrammingLab4.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // A helper property to get the full name for display
        public string FullName => $"{FirstName} {LastName}";
    }
}