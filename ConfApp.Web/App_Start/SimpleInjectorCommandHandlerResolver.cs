using System;
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

        public CommandHandler<TResult> ResolveForCommand<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var wrapperType = typeof(CommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = _container.GetInstance(handlerType);
            var wrapperHandler = Activator.CreateInstance(wrapperType, handler);
            return (CommandHandler<TResult>) wrapperHandler;
        }
    }
}