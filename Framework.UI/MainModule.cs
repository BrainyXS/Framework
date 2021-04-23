using Autofac;

namespace Framework.UI.Implementation
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<UIModule>();
            base.Load(builder);
        }
    }
}