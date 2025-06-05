using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ClassCompass.Shared.Data;
using ClassCompass.Shared.Services;
using ClassCompassApp.Views;


namespace ClassCompassApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        var connectionString = "Server=192.168.68.83;Port=3306;Database=ClassCompassDb;User=root;Password=Dahlia1525;";

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        builder.Services.AddScoped<AttendanceApi>();
        builder.Services.AddScoped<HomeworkApi>();
        builder.Services.AddScoped<GradesApi>();

        builder.Services.AddTransient<TeacherGradingPage>();
        builder.Services.AddTransient<AttendanceTrackingPage>();
        builder.Services.AddTransient<HomeworkSubmissionPage>();
        builder.Services.AddTransient<SchoolSignUpPage>();
        builder.Services.AddTransient<TeacherSignUpPage>();
        builder.Services.AddTransient<StudentSignUpPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<TeacherLoginPage>();
        builder.Services.AddTransient<StudentLoginPage>();
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        try
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!db.Database.CanConnect())
                Console.WriteLine("❌ Cannot connect to MySQL. Check your connection.");
            else
                Console.WriteLine("✅ Connected to MySQL successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ MySQL connection failed: {ex.Message}");
        }

        return app;
    }
}
