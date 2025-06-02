using AdvancedProgrammingLab4.Models; // Your correct namespace for models
using System; // Needed for Clipboard.GetData and Exception
using System.Collections.ObjectModel;
using System.Linq; // Needed for .ToList() and .Where()
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Needed for MouseButtonEventArgs, ApplicationCommands
using YourProjectName.Models;

namespace AdvancedProgrammingLab4
{
   
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


        // Handle double-click on an available student to move to selected [cite: 17]
        private void AvailableStudentsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Student selectedStudent = AvailableStudentsListView.SelectedItem as Student;
            if (selectedStudent != null)
            {
                AvailableStudents.Remove(selectedStudent);
                SelectedStudents.Add(selectedStudent);
            }
        }

        // Handle "Remove" button click for a selected student to move to available [cite: 18]
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

        // Handle "Add" or "Save" Course Button Click [cite: 13, 19]
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
            this.Close(); // Close the window
        }

        // --- Paste Student Logic (for AddCourseWindow) ---

        // Determines if the Paste command can be executed [cite: 2]
        private void PasteStudent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Command can execute if the clipboard contains data of type Student
            e.CanExecute = Clipboard.ContainsData(typeof(Student).FullName);
        }

        // Executes the Paste command [cite: 2, 6]
        private void PasteStudent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                // Retrieve the Student object from the clipboard
                Student pastedStudent = Clipboard.GetData(typeof(Student).FullName) as Student;

                if (pastedStudent != null)
                {
                    // Check if the student is already in selected students to avoid duplicates
                    if (SelectedStudents.Any(s => s.FirstName == pastedStudent.FirstName && s.LastName == pastedStudent.LastName))
                    {
                        MessageBox.Show($"'{pastedStudent.FullName}' is already in the selected students list.", "Paste Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Check if the student is in available students and remove them [cite: 2]
                    Student studentInAvailable = AvailableStudents.FirstOrDefault(s => s.FirstName == pastedStudent.FirstName && s.LastName == pastedStudent.LastName);
                    if (studentInAvailable != null)
                    {
                        AvailableStudents.Remove(studentInAvailable); // Remove from available list
                    }
                    // If not found in available, it means they were already moved or not initially in the list
                    // In either case, we proceed to add them to selected.

                    SelectedStudents.Add(pastedStudent); // Add to selected students
                    MessageBox.Show($"'{pastedStudent.FullName}' pasted to selected students.", "Paste Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to paste student: {ex.Message}", "Paste Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}