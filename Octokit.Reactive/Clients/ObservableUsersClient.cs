using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableUsersClient : IObservableUsersClient
    {
        readonly IUsersClient _client;

        public ObservableUsersClient(IUsersClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this._client = client;
        }

        public IObservable<User> Get(string login)
        {
            Ensure.ArgumentNotNull(login, "login");

            return _client.Get(login).ToObservable();
        }

        public IObservable<User> Current()
        {
            return _client.Current().ToObservable();
        }

        public IObservable<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return _client.Update(user).ToObservable();
        }

        public IObservable<IReadOnlyList<EmailAddress>> GetEmails()
        {
            return _client.GetEmails().ToObservable();
        }
    }
}
