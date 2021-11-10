using System;

namespace API_DesignPatterns.Core.Exceptions
{
    public class EntityAlreadyMarkedAsDeletedException : Exception
    {
        public EntityAlreadyMarkedAsDeletedException() : base() { }

        public EntityAlreadyMarkedAsDeletedException(string message) : base(message) { }
    }
}
