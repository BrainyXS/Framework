using System.ComponentModel;
using System.Runtime.CompilerServices;
using Framework.UI.Implementation;

namespace Framework.Contract.Navigation
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public  INavigationService Navigation { get; set; }

        public void InitializeParams(INavigationService navigationService)
        {
            Navigation = navigationService;
            Initialize();
        }

        public virtual void Initialize()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}