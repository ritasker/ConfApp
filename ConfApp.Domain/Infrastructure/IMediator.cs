namespace ConfApp.Domain.Infrastructure
{
    public interface IMediator
    {
        TResult Issue<TResult>(ICommand<TResult> command);
    }
}