namespace Framework.Contract.Navigation
{
    public interface INotifyOnNavigate
    {
        void NavigatedTo(NavigationContext navigationContext);
    }
}