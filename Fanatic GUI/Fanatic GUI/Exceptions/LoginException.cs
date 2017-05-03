using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanatic_GUI.Exceptions
{
    class LoginException : Exception
    {
        public LoginException(): base() {}
        public LoginException(string message) : base(message) { }
		public LoginException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
