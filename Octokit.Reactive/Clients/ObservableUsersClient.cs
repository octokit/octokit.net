﻿using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
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
            Ensure.ArgumentNotNull(login, "login");

            return client.Get(login).ToObservable();
        }

        public IObservable<User> Current()
        {
            return client.Current().ToObservable();
        }

        public IObservable<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return client.Update(user).ToObservable();
        }
    }
}
