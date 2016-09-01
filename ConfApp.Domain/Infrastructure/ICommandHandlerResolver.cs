using System.Windows.Input;

namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
    }
}