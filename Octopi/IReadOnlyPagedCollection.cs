﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi
{
    /// <summary>
    /// Reflects a collection of datat returned from an API that can be paged.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyPagedCollection<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Returns the next page of items.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        Task<IReadOnlyPagedCollection<T>> GetNextPage();
    }
}
