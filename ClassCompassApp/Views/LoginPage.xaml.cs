using ClassCompassApp.Views;

namespace ClassCompassApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnTeacherLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TeacherLoginPage));
    }

    private async void OnStudentLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(StudentLoginPage));
    }
}
