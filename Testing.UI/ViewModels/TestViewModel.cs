using System.Threading.Tasks;
using Framework.Contract.Navigation;

namespace Testing.UI.ViewModels
{
    public class TestViewModel : ViewModelBase, INotifyOnNavigate
    {
        public async Task NavigatedTo(NavigationContext navigationContext)
        {
            await Task.CompletedTask;
        }
    }
}