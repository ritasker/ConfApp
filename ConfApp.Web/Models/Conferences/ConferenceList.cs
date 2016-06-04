using System.Collections.Generic;

namespace ConfApp.Web.Models.Conferences
{
    public class ConferenceList
    {
        public ConferenceList()
        {
            Items = new List<ConferenceSummary> { new ConferenceSummary() };
        }
        public List<ConferenceSummary> Items { get; set; }
    }
}