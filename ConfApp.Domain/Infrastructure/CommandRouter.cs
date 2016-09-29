namespace ConfApp.Domain.Infrastructure
{
    public class CommandRouter : ICommandRouter
    {
        private readonly ICommandHandlerResolver _commandHandlerResolver;

        public CommandRouter(ICommandHandlerResolver commandHandlerResolver)
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