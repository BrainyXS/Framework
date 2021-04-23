using Autofac;
using Framework.UI.Implementation.ViewModels;

namespace Framework.UI.Implementation
{
    public class UIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WelcomeViewModel>();
            base.Load(builder);
        }
    }
}