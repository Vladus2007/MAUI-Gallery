using Gallery.Views;
using Microsoft.Maui.Controls;

namespace Gallery
{
    public partial class App : Application
    {
        public App(AppShell shell)
        {
            InitializeComponent();
            MainPage = shell; 
        }
    }
}