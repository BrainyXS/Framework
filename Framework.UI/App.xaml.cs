using System.Windows;
using System.Windows.Controls;
using Autofac;
using Framework.UI.Implementation.NavigationService;
using Testing.UI.ViewModels;
using Testing.UI.Views;

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
            var nav = new Navigation();
            var builder = new ContainerBuilder();
            builder.RegisterModule<UIModule>();
            builder.RegisterModule<Testing.UI.UIModlue>();
            nav.Setup(mainViewModel.FindName("Frame") as Frame, builder);
            nav.NavigateTo<TestViewModel>();
            mainViewModel.Show();
        }
    }
    
}