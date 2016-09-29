namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand message);
    }
}