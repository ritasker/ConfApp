using System;

namespace ConfApp.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}