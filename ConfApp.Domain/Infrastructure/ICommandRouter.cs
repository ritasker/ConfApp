namespace ConfApp.Domain.Infrastructure
{
    public interface ICommandRouter
    {
        TResult Issue<TResult>(ICommand<TResult> command);
    }
}