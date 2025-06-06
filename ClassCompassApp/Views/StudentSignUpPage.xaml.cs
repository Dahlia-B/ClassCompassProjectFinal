using ClassCompass.Shared.Data;
using ClassCompass.Shared.Models;

namespace ClassCompassApp.Views;

public partial class StudentSignUpPage : ContentPage
{
    private readonly AppDbContext _dbContext;

    public StudentSignUpPage(AppDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    private async void OnRegisterStudentClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(StudentNameEntry.Text) ||
                string.IsNullOrWhiteSpace(StudentIdEntry.Text) ||
                string.IsNullOrWhiteSpace(StudentPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(ClassIdEntry.Text) ||
                string.IsNullOrWhiteSpace(TeacherIdEntry.Text) ||
                string.IsNullOrWhiteSpace(TeacherPasswordEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (!int.TryParse(TeacherIdEntry.Text, out int teacherId))
            {
                await DisplayAlert("Error", "Teacher ID must be numeric", "OK");
                return;
            }

            var teacher = await _dbContext.Teachers.FindAsync(teacherId);
            if (teacher == null || !BCrypt.Net.BCrypt.Verify(TeacherPasswordEntry.Text, teacher.PasswordHash))
            {
                await DisplayAlert("Error", "Invalid teacher credentials", "OK");
                return;
            }

            if (!int.TryParse(StudentIdEntry.Text, out int studentId) ||
                !int.TryParse(ClassIdEntry.Text, out int classId))
            {
                await DisplayAlert("Error", "Student and Class ID must be numeric", "OK");
                return;
            }

            var student = new Student
            {
                Name = StudentNameEntry.Text.Trim(),
                StudentId = studentId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(StudentPasswordEntry.Text),
                Id = classId,
                TeacherId = teacher.Id
            };

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            await DisplayAlert("Success", "Student registered successfully!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}