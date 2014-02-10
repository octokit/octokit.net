using Octokit.Reactive.Internal;
using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableUserEmailsClient : IObservableUserEmailsClient
    {
        readonly IUserEmailsClient _client;
        readonly IConnection _connection;

        public ObservableUserEmailsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User.Email;
            _connection = client.Connection;
        }

        public IObservable<EmailAddress> GetAll()
        {
            return _connection.GetAndFlattenAllPages<EmailAddress>(ApiUrls.Emails());
        }

        public IObservable<string> Add(params string[] emailAddresses)
        {
            return _client.Add(emailAddresses).ToObservable().SelectMany(a => a);
        }
    }
}
