using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octopi.Reactive
{
    public interface IObservableAuthorizationsClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "It's an API call, so it's not a property.")]
        IObservable<IReadOnlyCollection<Authorization>> GetAll();
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        IObservable<Authorization> Get(long id);
        IObservable<Authorization> Update(long id, AuthorizationUpdate authorization);
        IObservable<Authorization> Create(AuthorizationUpdate authorization);
        IObservable<Unit> Delete(long id);
    }
}
