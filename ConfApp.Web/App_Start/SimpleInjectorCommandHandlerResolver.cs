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

        public CommandHandler<ICommand<TResult>, TResult> ResolveForCommand<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(CommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            return (CommandHandler<ICommand<TResult>, TResult>) _container.GetInstance(handlerType);
        }
    }
}