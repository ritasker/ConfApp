namespace ConfApp.Web.Models.Conferences
{
    using System.Collections.Generic;

    public class ConferenceList
    {
        public ConferenceList()
        {
            Items = new List<ConferenceSummary> {new ConferenceSummary()};
        }

        public List<ConferenceSummary> Items { get; set; }
    }
}