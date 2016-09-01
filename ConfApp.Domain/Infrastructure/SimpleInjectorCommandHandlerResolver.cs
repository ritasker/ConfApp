using System.Windows.Input;
using SimpleInjector;

namespace ConfApp.Domain.Infrastructure
{
    public class SimpleInjectorCommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly Container _container;

        public SimpleInjectorCommandHandlerResolver(Container container)
        {
            _container = container;
        }

        public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            return (ICommandHandler<TCommand>)_container.GetInstance(handlerType);
        }
    }
}