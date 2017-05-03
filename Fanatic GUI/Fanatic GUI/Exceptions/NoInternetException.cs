using System;

namespace Fanatic_GUI.Exceptions
{
    internal class NoInternetException : Exception
    {
        public NoInternetException()
        {
        }

        public NoInternetException(string message) : base(message)
        {
        }

        public NoInternetException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}