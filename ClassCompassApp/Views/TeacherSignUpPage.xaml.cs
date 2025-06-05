using ClassCompass.Shared.Data;
using ClassCompass.Shared.Models;
using ClassCompass.Shared.Services;


namespace ClassCompassApp.Views;

public partial class TeacherSignUpPage : ContentPage
{
    private readonly AppDbContext _dbContext;

    public TeacherSignUpPage(AppDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    private async void OnRegisterTeacherClicked(object sender, EventArgs e)
    {
        RegisterTeacherButton.IsEnabled = false;

        try
        {
            if (string.IsNullOrWhiteSpace(TeacherNameEntry.Text) ||
                string.IsNullOrWhiteSpace(TeacherIdEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(SubjectEntry.Text) ||
                string.IsNullOrWhiteSpace(SchoolIdEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (!int.TryParse(TeacherIdEntry.Text, out int teacherId) ||
                !int.TryParse(SchoolIdEntry.Text, out int schoolId))
            {
                await DisplayAlert("Error", "IDs must be numeric", "OK");
                return;
            }

            var hashedPassword = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(PasswordEntry.Text));

            var teacher = new Teacher
            {
                Name = TeacherNameEntry.Text.Trim(),
                TeacherId = teacherId,
                PasswordHash = hashedPassword,
                Subject = SubjectEntry.Text.Trim(),
                SchoolId = schoolId
            };

            await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();

            await DisplayAlert("Success", "Teacher registered successfully!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
        }
        finally
        {
            RegisterTeacherButton.IsEnabled = true;
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}