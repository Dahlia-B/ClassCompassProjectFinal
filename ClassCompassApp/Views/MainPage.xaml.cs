using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSchoolSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SchoolSignUpPage));
    }

    private async void OnTeacherSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TeacherSignUpPage));
    }

    private async void OnStudentSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(StudentSignUpPage));
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
