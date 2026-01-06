using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Project2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Do not set MainPage directly; CreateWindow is used to initialize the app window in MAUI.
            // MainPage = new NavigationPage(new MainPage());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}