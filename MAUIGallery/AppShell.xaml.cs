using Gallery.Views;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace Gallery
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            
                InitializeComponent();
                
                Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            
             
            
        }
    }
}