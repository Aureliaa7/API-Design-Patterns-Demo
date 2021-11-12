using System;

namespace API_DesignPatterns.Core.Exceptions
{
    public class FailedRegistrationException : Exception
    {
        public FailedRegistrationException() : base() { }

        public FailedRegistrationException(string message) : base(message) { }
    }
}
