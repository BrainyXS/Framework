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
        private UserControl _frame;
        private IContainer _container;
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
                var viewInstance = Activator.CreateInstance(viewType) as ContentControl;


                _frame.Content = viewInstance ??
                                 throw new ApplicationException($"View zu {viewModelType.Name} nicht gefunden");

                await viewModelInstance.InitializeParams(this);
                viewInstance.DataContext = viewModelInstance;
            }
            else
            {
                throw new Exception("Die View zu " + viewModelType.Name + " konnte nicht gefunden werden");
            }

            if (viewModelInstance is INotifyOnNavigate navigateable)
            {
                await navigateable.NavigatedTo(context);
            }
        }

        public void StartLoading()
        {
        }

        public void EndLoading()
        {
        }


        public void Setup(UserControl frame, ContainerBuilder builder, string assembly)
        {
            _container = builder.Build();
            _frame = frame;
            _assembly = assembly;
        }
    }
}