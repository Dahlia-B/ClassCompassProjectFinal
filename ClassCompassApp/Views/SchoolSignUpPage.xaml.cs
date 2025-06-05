using ClassCompass.Shared.Data;
using ClassCompass.Shared.Models;
using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views
{
    public partial class SchoolSignUpPage : ContentPage
    {
        private readonly AppDbContext _dbContext;

        // Constructor injection of AppDbContext
        public SchoolSignUpPage(AppDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnRegisterSchoolClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SchoolNameEntry.Text) ||
                    string.IsNullOrWhiteSpace(SchoolIdEntry.Text) ||
                    string.IsNullOrWhiteSpace(ClassCountEntry.Text))
                {
                    await DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }

                if (!int.TryParse(SchoolIdEntry.Text, out int schoolId))
                {
                    await DisplayAlert("Error", "School ID must be numeric.", "OK");
                    return;
                }

                if (!int.TryParse(ClassCountEntry.Text, out int classCount))
                {
                    await DisplayAlert("Error", "Number of classes must be numeric.", "OK");
                    return;
                }

                var school = new School
                {
                    Name = SchoolNameEntry.Text.Trim(),
                    SchoolId = schoolId,
                    NumberOfClasses = classCount,
                };

                await _dbContext.Schools.AddAsync(school);
                await _dbContext.SaveChangesAsync();

                await DisplayAlert("Success", "School registered successfully!", "OK");
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
}
