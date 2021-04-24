using Autofac;
using Framework.Contract.Navigation;
using Framework.UI.Implementation.NavigationService;

namespace Framework.UI.Implementation
{
    public class UIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Navigation>().As<INavigationService>();
            base.Load(builder);
        }
    }
}