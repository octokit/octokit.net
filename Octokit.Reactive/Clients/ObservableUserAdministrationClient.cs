using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableUserAdministrationClient : IObservableUserAdministrationClient
    {
        public IObservable<Unit> Demote(string login)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Promote(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            throw new NotImplementedException();
        }

        public IObservable<Unit> Suspend(string login)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Unsuspend(string login)
        {
            throw new NotImplementedException();
        }
    }
}
