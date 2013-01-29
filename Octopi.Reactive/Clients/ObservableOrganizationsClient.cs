﻿using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octopi.Reactive.Clients
{
    public class ObservableOrganizationsClient : IObservableOrganizationsClient
    {
        readonly IOrganizationsClient client;

        public ObservableOrganizationsClient(IOrganizationsClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public IObservable<Organization> Get(string org)
        {
            return client.Get(org).ToObservable();
        }

        public IObservable<IReadOnlyCollection<Organization>> GetAllForCurrent()
        {
            return client.GetAllForCurrent().ToObservable();
        }

        public IObservable<IReadOnlyCollection<Organization>> GetAll(string user)
        {
            return client.GetAll(user).ToObservable();
        }
    }
}
