using System.Windows.Input;

namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}