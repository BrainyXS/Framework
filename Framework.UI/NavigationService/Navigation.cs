using System;
using System.Windows.Controls;
using Autofac;

namespace Framework.UI.Implementation.NavigationService
{
    public static class Navigation
    {
        private static Frame _frame;
        private static IContainer _container;

        public static void NavigateTo<T>() where T : ViewModelBase
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


        public static void Setup(Frame frame)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<UIModule>();
            _container = builder.Build();
            _frame = frame;
        }
    }
}