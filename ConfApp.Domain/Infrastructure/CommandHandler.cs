namespace ConfApp.Domain.Infrastructure
{
    public abstract class CommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        public abstract TResult Handle(ICommand<TResult> message);
    }
}