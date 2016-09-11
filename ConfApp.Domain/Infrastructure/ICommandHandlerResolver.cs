namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandHandlerResolver
    {
        CommandHandler<ICommand<TResult>, TResult> ResolveForCommand<TResult>(ICommand<TResult> command);
    }
}