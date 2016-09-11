using SimpleInjector;

namespace ConfApp.Domain.Infrastructure
{
    public class Mediator : IMediator
    {
        private readonly ICommandHandlerResolver _commandHandlerResolver;
        private readonly Container _container;

        public Mediator(ICommandHandlerResolver commandHandlerResolver)
        {
            _commandHandlerResolver = commandHandlerResolver;
        }

        public TResult Issue<TResult>(ICommand<TResult> command)
        {
            var handler = _commandHandlerResolver.ResolveForCommand(command);
            return handler.Handle(command);
        }
    }
}