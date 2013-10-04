using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableRepositoriesClient : IObservableRepositoriesClient
    {
        readonly IRepositoriesClient _client;

        public ObservableRepositoriesClient(IRepositoriesClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            
            _client = client;
        }

        public IObservable<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name).ToObservable();
        }

        public IObservable<IReadOnlyCollection<Repository>> GetAllForCurrent()
        {
            return _client.GetAllForCurrent().ToObservable();
        }

        public IObservable<IReadOnlyCollection<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.GetAllForUser(login).ToObservable();
        }

        public IObservable<IReadOnlyCollection<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return _client.GetAllForOrg(organization).ToObservable();
        }

        public IObservable<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetReadme(owner, name).ToObservable();
        }
    }
}
