using BCrypt.Net;

using ClassCompass.Shared.Data;
using ClassCompass.Shared.Models;
using ClassCompass.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System;

namespace ClassCompassApp.Views
{
    public partial class TeacherLoginPage : ContentPage
    {
        private readonly AppDbContext _dbContext;

        public TeacherLoginPage(AppDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Defensive null check if controls are not assigned properly
                if (TeacherIdEntry == null || PasswordEntry == null)
                {
                    await DisplayAlert("Error", "Login controls are not initialized", "OK");
                    return;
                }

                var teacherIdText = TeacherIdEntry.Text;
                var passwordText = PasswordEntry.Text;

                if (string.IsNullOrWhiteSpace(teacherIdText) || string.IsNullOrWhiteSpace(passwordText))
                {
                    await DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }

                if (!int.TryParse(teacherIdText, out int teacherId))
                {
                    await DisplayAlert("Error", "Teacher ID must be a number", "OK");
                    return;
                }

                var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacherId);

                if (teacher != null && BCrypt.Net.BCrypt.Verify(passwordText, teacher.PasswordHash))
                {
                    // Navigate to TeacherDashboard (make sure route is registered)
                    await Shell.Current.GoToAsync($"//{nameof(TeacherDashboard)}");
                }
                else
                {
                    await DisplayAlert("Login Failed", "Invalid teacher ID or password", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
