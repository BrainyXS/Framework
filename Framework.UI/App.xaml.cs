using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Framework.Contract.Navigation;
using Framework.UI.Implementation.NavigationService;

namespace Framework.UI.Implementation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static INavigationService Open(Module m, string assembly)
        {
            var mainViewModel = new MainWindow();
            var nav = new Navigation();
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            builder.RegisterModule<UIModule>();
            builder.RegisterModule(m);
            nav.Setup(mainViewModel.Control, builder, assembly);
            mainViewModel.Show();
            return nav;
        }
    }
}