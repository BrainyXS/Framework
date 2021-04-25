using System.Windows;
using Autofac;
using Framework.UI.Implementation.NavigationService;
using Testing.UI.ViewModels;

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
            nav.Setup(mainViewModel, builder);
            nav.NavigateTo<TestViewModel>();
            mainViewModel.Show();
        }
    }
    
}