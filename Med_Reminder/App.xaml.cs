using System.Security.Cryptography.X509Certificates;

namespace Med_Reminder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());

            
        }
        public static int CurrentUserId { get; set; }
        public static bool IsUserLoggedIn { get; set; }

    }
}