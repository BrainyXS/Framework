using System.Windows;
using Testing.UI.ViewModels;

namespace Testing.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var nav = Framework.UI.Implementation.App.Open(new UIModule(), "Testing.UI");
            await nav.NavigateTo<TestViewModel>();
        }
    }
}