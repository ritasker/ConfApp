using System.Windows.Input;
using SimpleInjector;

namespace ConfApp.Domain.Infrastructure
{
    public class Mediator
    {
        private readonly ICommandHandlerResolver _commandHandlerResolver;
        private readonly Container _container;

        public Mediator(ICommandHandlerResolver commandHandlerResolver)
        {
            _commandHandlerResolver = commandHandlerResolver;
        }

        public void Issue<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _commandHandlerResolver.ResolveForCommand<TCommand>();
            handler.Handle(command);
        }
    }
}