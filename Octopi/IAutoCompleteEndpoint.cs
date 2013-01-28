using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi
{
    public interface IAutoCompleteEndpoint
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        Task<IReadOnlyDictionary<string, Uri>> GetEmojis();
    }
}
