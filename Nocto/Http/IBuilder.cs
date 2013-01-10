using System;
using System.Collections.Generic;

namespace Nocto.Http
{
    /// <summary>
    /// A <see cref="IBuilder"/>'s job is to build up the "application" for a connection.
    /// </summary>
    public interface IBuilder
    {
        IReadOnlyList<Func<IApplication, IApplication>> Handlers { get; }
        IApplication Run(IApplication adapter);
        void Use(Func<IApplication, IApplication> handler);
    }
}
