using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableRepositoriesClient : IObservableRepositoriesClient
    {
        readonly IRepositoriesClient client;

        public ObservableRepositoriesClient(IRepositoriesClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            
            this.client = client;
        }

        public IObservable<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return client.Get(owner, name).ToObservable();
        }

        public IObservable<IReadOnlyCollection<Repository>> GetAllForCurrent()
        {
            return client.GetAllForCurrent().ToObservable();
        }

        public IObservable<IReadOnlyCollection<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return client.GetAllForUser(login).ToObservable();
        }

        public IObservable<IReadOnlyCollection<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return client.GetAllForOrg(organization).ToObservable();
        }

        public IObservable<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return client.GetReadme(owner, name).ToObservable();
        }
    }
}
