using System;

namespace API_DesignPatterns.Core.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base() { }

        public InvalidCredentialsException(string message) : base(message) { }
    }
}
