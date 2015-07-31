using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Provides a property for the Last recorded API infomation
    /// </summary>
    public interface IApiInfo
    {
        /// <summary>
        /// Gets the latest API Info - this will be null if no API calls have been made
        /// </summary>
        /// <returns><seealso cref="ApiInfo"/> representing the information returned as part of an Api call</returns>
        ApiInfo LastApiInfo { get; }
    }
}
