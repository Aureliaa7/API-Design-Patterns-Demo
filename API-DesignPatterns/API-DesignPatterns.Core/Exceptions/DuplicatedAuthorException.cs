using System;

namespace API_DesignPatterns.Core.Exceptions
{
    public class DuplicatedAuthorException : Exception
    {
        public DuplicatedAuthorException() : base() { }

        public DuplicatedAuthorException(string message) : base(message) { }
    }
}
