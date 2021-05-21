using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Framework.UI.Implementation;

namespace Framework.Contract.Navigation
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigationService Navigation { get; set; }

        public async Task InitializeParams(INavigationService navigationService)
        {
            Navigation = navigationService;
            await Initialize();
        }

        public async Task<T> LoadTask<T>(Task<T> task)
        {
            Navigation.StartLoading();
            var red = await (await task.ContinueWith(async task =>
                {
                    var result = await task;
                    Navigation.EndLoading();
                    return result;
                },
                CancellationToken.None, TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false));
            return red;
        }

        public async Task LoadTask(Task task)
        {
            Navigation.StartLoading();
            await task.ContinueWith(_ => { Navigation.EndLoading(); },
                CancellationToken.None, TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);
        }

        public async virtual Task Initialize()
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}