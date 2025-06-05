using ClassCompass.Shared.Data;
using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views;

public partial class StudentLoginPage : ContentPage
{
    private readonly AppDbContext _db;

    public StudentLoginPage(AppDbContext db)
    {
        InitializeComponent();
        _db = db;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(StudentIdEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        int studentId = int.Parse(StudentIdEntry.Text);
        var student = await _db.Students.FindAsync(studentId);

        if (student != null && BCrypt.Net.BCrypt.Verify(PasswordEntry.Text, student.PasswordHash))
        {
            await Shell.Current.GoToAsync($"//StudentDashboardPage?studentId={studentId}");
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid student ID or password", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
