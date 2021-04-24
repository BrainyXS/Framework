using System;
using System.Windows.Controls;
using Autofac;
using Framework.Contract;
using Framework.Contract.Navigation;

namespace Framework.UI.Implementation.NavigationService
{
    public class Navigation : INavigationService
    {
        private Frame _frame;
        private IContainer _container;

        public void NavigateTo<T>() where T : ViewModelBase
        {
            var viewModelType = typeof(T);
            var viewModelInstance = _container.Resolve<T>();
            var viewName = viewModelType.Name.Substring(0, viewModelType.Name.Length - "Model".Length);
            var viewType = Type.GetType("Framework.UI.Views." + viewName);

            if (viewType != null)
            {
                var viewInstance = Activator.CreateInstance(viewType) as Page;
                if (viewInstance == null)
                {
                    throw new ApplicationException($"View zu {viewModelType.Name} nicht gefunden");
                }
                viewModelInstance.InitializeParams(this);
                viewInstance.DataContext = viewModelInstance;

                _frame.Navigate(viewInstance);
                _frame.NavigationService.Refresh();
            }

            var navigateable = viewModelInstance as INotifyOnNavigate;
            if (navigateable != null)
            {
                navigateable.NavigatedTo();
            }
        }


        public void Setup(Frame frame, ContainerBuilder builder)
        {
            _container = builder.Build();
            _frame = frame;
        }
    }
}