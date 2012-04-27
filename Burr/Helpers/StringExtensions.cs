using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr
{
    public static class StringExtensions
    {
        public static bool IsBlank(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        public static bool IsNotBlank(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }
}
