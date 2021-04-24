using Autofac;
using Testing.UI.ViewModels;

namespace Testing.UI
{
    public class UIModlue : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestViewModel>();
            base.Load(builder);
        }
    }
}