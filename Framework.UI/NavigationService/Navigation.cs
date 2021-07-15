using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using Autofac;
using Framework.Contract.Navigation;

namespace Framework.UI.Implementation.NavigationService
{
    public class Navigation : INavigationService
    {
        private MainWindow _frame;
        private IContainer _container;
        private Page _view;
        private string _assembly;

        public async Task NavigateTo<T>(params object[] parameter) where T : ViewModelBase
        {
            var context = new NavigationContext();
            foreach (var param in parameter)
            {
                context.Params.Add(new KeyValuePair<Type, object>(param.GetType(), param));
            }

            var viewModelType = typeof(T);
            var viewModelInstance = _container.Resolve<T>();
            var viewName = viewModelType.Name.Substring(0, viewModelType.Name.Length - "Model".Length);
            var viewType = Type.GetType($"{_assembly}.Views." + viewName + $", {_assembly}");

            if (viewType != null)
            {
                var viewInstance = Activator.CreateInstance(viewType) as Page;
                if (viewInstance == null)
                {
                    throw new ApplicationException($"View zu {viewModelType.Name} nicht gefunden");
                }

                _frame.Navigate(viewInstance);

                await viewModelInstance.InitializeParams(this);
                viewInstance.DataContext = viewModelInstance;
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

        public void StartLoading()
        {
            _view = _frame.Content as Page;
            _frame.Navigate(new LoadingPage());
        }

        public void EndLoading()
        {
            _frame.Navigate(_view);
            _view = null;
        }


        public void Setup(MainWindow frame, ContainerBuilder builder, string assembly)
        {
            _container = builder.Build();
            _frame = frame;
            _assembly = assembly;
        }
    }
}