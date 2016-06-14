using System;

namespace ConfApp.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, string id, Exception ex = null) : base($"A {type} with {id} could not be found", ex)
        {
        }
    }
}