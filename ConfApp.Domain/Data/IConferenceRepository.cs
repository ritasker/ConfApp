namespace ConfApp.Domain.Data
{
    using System.Linq;
    using Models;

    public interface IConferenceRepository
    {
        IQueryable<Conference> Query();

        Conference Save(Conference conference);
    }
}