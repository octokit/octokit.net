using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Used for date and int comparisions in searches
    /// </summary>
    public enum SearchQualifierOperator
    {
        GreaterThan, // >
        LessThan, // <
        LessThanOrEqualTo, // <=
        GreaterThanOrEqualTo// >=
    }
}
