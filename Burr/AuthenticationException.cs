using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message)
            : base(message)
        { }
    }
}
