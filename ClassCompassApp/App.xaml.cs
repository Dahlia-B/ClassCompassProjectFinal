using ClassCompass.Shared.Models;

namespace ClassCompassApp;

public partial class App : Application
{
    public class UserModel
    {
        public string UserId { get; set; } = string.Empty;
    }

    public static UserModel? CurrentUser { get; set; }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell(); 
    }
}
