using AdvancedProgrammingLab4.Models;
using System.Collections.ObjectModel; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YourProjectName.Models; 

namespace AdvancedProgrammingLab4
{    
    public partial class MainWindow : Window
    {        
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Course> Courses { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the collections
            Students = new ObservableCollection<Student>();
            Teachers = new ObservableCollection<Teacher>();
            Courses = new ObservableCollection<Course>();

            // Set the DataContext of the window to itself. This allows XAML bindings
            // to directly access properties of this MainWindow class.
            this.DataContext = this;

            // Optional: Add some dummy data for testing
            Students.Add(new Student { FirstName = "Cedric", LastName = "Coltrane" });
            Students.Add(new Student { FirstName = "Hank", LastName = "Spencer" });
            Students.Add(new Student { FirstName = "Rick", LastName = "Bennet" });

            Teachers.Add(new Teacher { FirstName = "John", LastName = "Doe" });
            Teachers.Add(new Teacher { FirstName = "Jane", LastName = "Smith" });
        }

        // --- Students Tab Logic ---
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            // Get text from the input fields
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();

            // Basic validation
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please enter both first name and last name for the student.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create a new Student object
            Student newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Add the new student to the ObservableCollection
            Students.Add(newStudent);

            // Clear the input fields after adding
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
        }

        // Event handler for the 'Remove' button within a Student ListView item
        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            // The sender is the Button that was clicked. Its DataContext will be the Student object.
            Button button = sender as Button;
            if (button != null)
            {
                Student studentToRemove = button.DataContext as Student;
                if (studentToRemove != null)
                {
                    Students.Remove(studentToRemove);
                }
            }
        }

        // --- Teachers Tab Logic (initial setup) ---
        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TeacherFirstNameTextBox.Text.Trim();
            string lastName = TeacherLastNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please enter both first name and last name for the teacher.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Teacher newTeacher = new Teacher
            {
                FirstName = firstName,
                LastName = lastName
            };

            Teachers.Add(newTeacher);

            TeacherFirstNameTextBox.Clear();
            TeacherLastNameTextBox.Clear();
        }

        private void RemoveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Teacher teacherToRemove = button.DataContext as Teacher;
                if (teacherToRemove != null)
                {
                    Teachers.Remove(teacherToRemove);
                }
            }
        }
              

        private void RemoveCourseButton_Click(object sender, RoutedEventArgs e)
        {
            // The sender is the Button that was clicked. Its DataContext will be the Course object.
            Button button = sender as Button;
            if (button != null)
            {
                Course courseToRemove = button.DataContext as Course;
                if (courseToRemove != null)
                {
                    Courses.Remove(courseToRemove);
                }
            }
        }
                
        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {            
            AddCourseWindow addCourseWindow = new AddCourseWindow(Students, Teachers);

            // Show the window as a dialog. This means execution pauses here until the dialog is closed.
            bool? result = addCourseWindow.ShowDialog();

            // Check if the dialog was closed with DialogResult = true (meaning "Add" or "Save" was clicked)
            if (result == true)
            {
                // Retrieve the new course object from the dialog window
                Course newCourse = addCourseWindow.CurrentCourse;
                if (newCourse != null)
                {
                    Courses.Add(newCourse); // Add the new course to our main Courses collection
                }
            }
        }

        // Event handler for double-clicking a course in the ListView to edit it
        private void CoursesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected course from the ListView
            Course selectedCourse = CoursesListView.SelectedItem as Course;

            if (selectedCourse != null)
            {
                // Open the AddCourseWindow in edit mode, passing the selected course
                // and the global lists of students and teachers
                AddCourseWindow editCourseWindow = new AddCourseWindow(selectedCourse, Students, Teachers);

                // Show the window as a dialog
                bool? result = editCourseWindow.ShowDialog();

                // If the dialog was closed with DialogResult = true (meaning "Save" was clicked)
                if (result == true)
                {
                    
                }
            }
        }
                
    }
}