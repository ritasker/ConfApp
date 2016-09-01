using System.Windows.Input;

namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<TCommand> ResolveForCommand<TCommand>() where TCommand : ICommand;
    }
}