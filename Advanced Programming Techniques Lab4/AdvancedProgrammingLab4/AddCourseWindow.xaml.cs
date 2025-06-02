using AdvancedProgrammingLab4.Models;
using System.Collections.ObjectModel;
using System.Linq; // Needed for .ToList() and .Where()
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Needed for MouseButtonEventArgs
using YourProjectName.Models; // Make sure to add this using directive

namespace AdvancedProgrammingLab4
{
    /// <summary>
    /// Interaction logic for AddCourseWindow.xaml
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        // Properties to hold the data being managed in this window
        public Course CurrentCourse { get; set; }
        public ObservableCollection<Student> AvailableStudents { get; set; }
        public ObservableCollection<Student> SelectedStudents { get; set; }
        public ObservableCollection<Teacher> AllTeachers { get; set; }

        // A flag to indicate if we are editing an existing course or adding a new one
        private bool _isEditMode = false;

        // Constructor for adding a new course
        public AddCourseWindow(ObservableCollection<Student> allStudents, ObservableCollection<Teacher> allTeachers)
        {
            InitializeComponent();

            // Initialize collections. We clone allStudents so we don't modify the main list directly.
            AvailableStudents = new ObservableCollection<Student>(allStudents);
            SelectedStudents = new ObservableCollection<Student>();
            AllTeachers = allTeachers; // Reference to the main teachers list

            CurrentCourse = new Course(); // Create a new empty course object

            // Set DataContext for the window to itself for binding
            this.DataContext = this;

            // Bind the ListViews and ComboBox
            AvailableStudentsListView.ItemsSource = AvailableStudents;
            SelectedStudentsListView.ItemsSource = SelectedStudents;
            TeacherComboBox.ItemsSource = AllTeachers;

            // Set the button text to "Add" for new course creation
            ActionCourseButton.Content = "Add";
            this.Title = "Add New Course";
        }

        // Constructor for editing an existing course [cite: 14]
        public AddCourseWindow(Course courseToEdit, ObservableCollection<Student> allStudents, ObservableCollection<Teacher> allTeachers)
        {
            InitializeComponent();

            _isEditMode = true;
            CurrentCourse = courseToEdit; // Work directly with the passed course object

            // Initialize collections for student selection
            // SelectedStudents should contain students already enrolled in the course
            SelectedStudents = new ObservableCollection<Student>(courseToEdit.EnrolledStudents);

            // AvailableStudents are all students minus those already selected
            AvailableStudents = new ObservableCollection<Student>(
                allStudents.Except(courseToEdit.EnrolledStudents).ToList()
            );

            AllTeachers = allTeachers; // Reference to the main teachers list

            // Set DataContext for the window to itself for binding
            this.DataContext = this;

            // Bind UI elements
            CourseNameTextBox.Text = CurrentCourse.Name;
            TeacherComboBox.ItemsSource = AllTeachers;
            TeacherComboBox.SelectedItem = CurrentCourse.AssignedTeacher; // Select the assigned teacher

            AvailableStudentsListView.ItemsSource = AvailableStudents;
            SelectedStudentsListView.ItemsSource = SelectedStudents;

            // Set the button text to "Save" for editing [cite: 15]
            ActionCourseButton.Content = "Save";
            this.Title = $"Edit Course: {CurrentCourse.Name}";
        }


        // Handle double-click on an available student to move to selected [cite: 11]
        private void AvailableStudentsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Student selectedStudent = AvailableStudentsListView.SelectedItem as Student;
            if (selectedStudent != null)
            {
                AvailableStudents.Remove(selectedStudent);
                SelectedStudents.Add(selectedStudent);
            }
        }

        // Handle "Remove" button click for a selected student to move to available [cite: 12]
        private void RemoveSelectedStudent_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Student studentToRemove = button.DataContext as Student;
                if (studentToRemove != null)
                {
                    SelectedStudents.Remove(studentToRemove);
                    AvailableStudents.Add(studentToRemove);
                }
            }
        }

        // Handle "Add" or "Save" Course Button Click [cite: 13]
        private void ActionCourseButton_Click(object sender, RoutedEventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(CourseNameTextBox.Text))
            {
                MessageBox.Show("Please enter a course name.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (TeacherComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a teacher for the course.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update CurrentCourse object with new/edited data
            CurrentCourse.Name = CourseNameTextBox.Text.Trim();
            CurrentCourse.AssignedTeacher = TeacherComboBox.SelectedItem as Teacher;

            // Clear and then add all selected students to the CurrentCourse's EnrolledStudents
            CurrentCourse.EnrolledStudents.Clear();
            foreach (var student in SelectedStudents)
            {
                CurrentCourse.EnrolledStudents.Add(student);
            }

            // Set DialogResult to true to indicate successful operation and close the window
            this.DialogResult = true;
            this.Close(); // Close the window [cite: 13]
        }
    }
}