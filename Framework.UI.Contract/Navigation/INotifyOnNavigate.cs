using System.Threading.Tasks;

namespace Framework.Contract.Navigation
{
    public interface INotifyOnNavigate
    {
        Task NavigatedTo(NavigationContext navigationContext);
    }
}