using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableUsersClient : IObservableUsersClient
    {
        readonly IUsersClient _client;
        readonly IConnection _connection;

        public ObservableUsersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User;
            _connection = client.Connection;
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

        public IObservable<EmailAddress> GetEmails()
        {
            return _connection.GetAndFlattenAllPages<EmailAddress>(ApiUrls.Emails());
        }
    }
}
