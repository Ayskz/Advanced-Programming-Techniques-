using AdvancedProgrammingLab4.Models;
using System.Collections.ObjectModel; // Needed for ObservableCollection

namespace YourProjectName.Models
{
    public class Course
    {
        public string Name { get; set; }
        public Teacher AssignedTeacher { get; set; } // Will hold a Teacher object

        // Use ObservableCollection to automatically update the UI when students are added/removed
        public ObservableCollection<Student> EnrolledStudents { get; set; } = new ObservableCollection<Student>();

        // A helper property to get the display string for a course
        public string DisplayText => $"{Name} Teacher: {AssignedTeacher?.FullName ?? "N/A"}";
    }
}