namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandHandlerResolver
    {
        CommandHandler<TResult> ResolveForCommand<TResult>(ICommand<TResult> command);
    }
}