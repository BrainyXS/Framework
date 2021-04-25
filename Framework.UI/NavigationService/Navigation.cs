using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Autofac;
using Framework.Contract.Navigation;

namespace Framework.UI.Implementation.NavigationService
{
    public class Navigation : INavigationService
    {
        private MainWindow _frame;
        private IContainer _container;

        public void NavigateTo<T>(params object[] parameter) where T : ViewModelBase
        {
            var context = new NavigationContext();
            foreach (var param in parameter)
            {
                context.Params.Add(new KeyValuePair<Type, object>(param.GetType(), param));
            }

            var viewModelType = typeof(T);
            var viewModelInstance = _container.Resolve<T>();
            var viewName = viewModelType.Name.Substring(0, viewModelType.Name.Length - "Model".Length);
            var viewType = Type.GetType("Lifecycle.UI.Views." + viewName + ", Lifecycle.UI");

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
            }
            else
            {
                throw new Exception("Die View zu " + viewModelType.Name + " konnte nicht gefunden werden");
            }

            var navigateable = viewModelInstance as INotifyOnNavigate;
            if (navigateable != null)
            {
                navigateable.NavigatedTo(context);
            }
        }


        public void Setup(MainWindow frame, ContainerBuilder builder)
        {
            _container = builder.Build();
            _frame = frame;
        }
    }
}