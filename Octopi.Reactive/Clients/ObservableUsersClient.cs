using System;
using System.Reactive.Threading.Tasks;

namespace Octopi.Reactive.Clients
{
    public class ObservableUsersClient : IObservableUsersClient
    {
        readonly IUsersClient client;

        public ObservableUsersClient(IUsersClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public IObservable<User> Get(string login)
        {
            return client.Get(login).ToObservable();
        }

        public IObservable<User> Current()
        {
            return client.Current().ToObservable();
        }

        public IObservable<User> Update(UserUpdate user)
        {
            return client.Update(user).ToObservable();
        }
    }
}
