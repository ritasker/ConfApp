using System;

namespace ConfApp.Domain.Models
{
    public class ConferenceDetails : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}