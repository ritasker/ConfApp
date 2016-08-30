namespace ConfApp.Domain.Models
{
    using System;

    public class Conference : Entity
    {
        public Conference(ConferenceDetails conference)
        {
            Name = conference.Name;
            Description = conference.Description;
            StartDate = conference.StartDate;
            EndDate = conference.EndDate;
        }

        public Conference() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}