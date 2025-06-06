using ClassCompass.Shared.Services;
using ClassCompass.Shared.Data;
using Microsoft.Maui.Controls;
using ClassCompass.Shared.Models;

namespace ClassCompassApp.Views
{
    public partial class TeacherGradingPage : ContentPage
    {
        private readonly GradesApi _gradesApi;

        public TeacherGradingPage(AppDbContext dbContext)
        {
            InitializeComponent();
            _gradesApi = new GradesApi(dbContext);
        }

        private async void OnSubmitGradeClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(ScoreEntry.Text, out int score))
            {
                await DisplayAlert("Error", "Please enter a valid numeric score.", "OK");
                return;
            }

            if (!int.TryParse(StudentIdEntry.Text, out int studentId))
            {
                await DisplayAlert("Error", "Please enter a valid numeric student ID.", "OK");
                return;
            }

            var grade = new Grade
            {
                StudentId = studentId,
                Score = score,
                // add other properties if needed, e.g., AssignmentId, DateGraded etc.
            };

            var result = await _gradesApi.AssignGrade(grade);

            if (result != null)
            {
                await DisplayAlert("Success", "Grade submitted successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to submit grade.", "OK");
            }
        }


    }
}