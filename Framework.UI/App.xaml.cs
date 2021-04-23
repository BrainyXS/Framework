using System.Windows;
using System.Windows.Controls;
using Framework.UI.Implementation.NavigationService;
using Framework.UI.Implementation.ViewModels;

namespace Framework.UI.Implementation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainViewModel = new MainWindow();
            Navigation.Setup(mainViewModel.FindName("Frame") as Frame);
            Navigation.NavigateTo<WelcomeViewModel>();
            mainViewModel.Show();
        }
    }
    
}