using System.Windows.Input;
using ConfApp.Domain.Infrastructure;
using SimpleInjector;

namespace ConfApp.Web
{
    public class SimpleInjectorCommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly Container _container;

        public SimpleInjectorCommandHandlerResolver(Container container)
        {
            _container = container;
        }

        public ICommandHandler<TCommand> ResolveForCommand<TCommand>() where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            return (ICommandHandler<TCommand>)_container.GetInstance(handlerType);
        }
    }
}