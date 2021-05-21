using System.Threading.Tasks;

namespace Framework.Contract.Navigation
{
    public interface INavigationService
    {
        public Task NavigateTo<T>(params object[] parameter) where T : ViewModelBase;
        void StartLoading();
        void EndLoading();
    }
}