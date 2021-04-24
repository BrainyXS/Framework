namespace Framework.Contract.Navigation
{
    public interface INavigationService
    {
        public void NavigateTo<T>(params object[] parameter) where T : ViewModelBase;
    }
}