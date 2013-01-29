﻿using System;
using Octopi.Clients;
using Octopi.Http;

namespace Octopi
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient : IGitHubClient
    {
        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to 
        /// https://api.github.com/
        /// </summary>
        public GitHubClient() : this(new Connection())
        {
        }

        public GitHubClient(ICredentialStore credentialStore) : this(new Connection(credentialStore))
        {
        }

        public GitHubClient(Uri baseAddress) : this(new Connection(baseAddress))
        {
        }

        public GitHubClient(ICredentialStore credentialStore, Uri baseAddress)
            : this(new Connection(baseAddress, credentialStore))
        {
        }

        public GitHubClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
            Authorization = new AuthorizationsClient(new ApiConnection<Authorization>(connection));
            AutoComplete = new AutoCompleteClient(connection);
            Organization = new OrganizationsClient(new ApiConnection<Organization>(connection));
            Repository = new RepositoriesClient(new ApiConnection<Repository>(connection));
            User = new UsersClient(new ApiConnection<User>(connection));
            SshKey = new SshKeysClient(new ApiConnection<SshKey>(connection));
        }

        /// <summary>
        /// Convenience property for getting and setting credentials.
        /// </summary>
        /// <remarks>
        /// Setting the credentials will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        public Credentials Credentials
        {
            get { return Connection.Credentials; }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                Connection.Credentials = value;
            }
        }

        /// <summary>
        /// The base address of the GitHub API. This defaults to https://api.github.com,
        /// but you can change it if needed (to talk to a GitHub:Enterprise server for instance).
        /// </summary>
        public Uri BaseAddress
        {
            get { return Connection.BaseAddress; }
        }

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnection Connection { get; private set; }

        public IAuthorizationsClient Authorization { get; private set; }
        public IAutoCompleteClient AutoComplete { get; private set; }
        public IOrganizationsClient Organization { get; private set; }
        public IRepositoriesClient Repository { get; private set; }
        public ISshKeysClient SshKey { get; private set; }
        public IUsersClient User { get; private set; }
    }
}
