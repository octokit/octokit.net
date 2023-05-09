using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuditLogEvent
    {
        public AuditLogEvent() { }

        public AuditLogEvent(string action, bool? active, bool? activeWas, string actor, long? actorId, string actorIp, object actorLocation,
            string blockedUser, string business, long? businessId,
            object config, object configWas, string contentType, long createdAt,
            object data, string deployKeyFingerprint, string documentId,
            string emoji, object events, object eventsWere, string explanation,
            string fingerPrint,
            string hashedToken, long? hookId,
            bool? limitedAvailability,
            string message,
            string name,
            string oldUser, string opensshPublicKey, string operationType, IReadOnlyList<string> org, IReadOnlyList<long> orgId,
            string previousVisibility, bool? publicRepo, long? pullRequestId, string pullRequestTitle, string pullRequestUrl,
            bool? readOnly, string repo, long? repoId, string repository, bool? repositoryPublic,
            string targetLogin, string team, long? transportProtocol, string transportProtocolName, long timestamp, 
            string user, string userAgent, long? userId,
            string visibility)
        {
            Action = action;
            Active = active;
            ActiveWas = activeWas;
            Actor = actor;
            ActorId = actorId;
            ActorIp = actorIp;
            ActorLocation = actorLocation;
            BlockedUser = blockedUser;
            Business = business;
            BusinessId = businessId;
            Config = config;
            ConfigWas = configWas;
            ContentType = contentType;
            CreatedAt = createdAt;
            Data = data;
            DeployKeyFingerprint = deployKeyFingerprint;
            DocumentId = documentId;
            Emoji = emoji;
            Events = events;
            EventsWere = eventsWere;
            Explanation = explanation;
            Fingerprint = fingerPrint;
            HashedToken = hashedToken;
            HookId = hookId;
            LimitedAvailability = limitedAvailability;
            Message = message;
            Name = name;
            OldUser = oldUser;
            OpensshPublicKey = opensshPublicKey;
            OperationType = operationType;
            Org = org;
            OrgId = orgId;
            PreviousVisibility = previousVisibility;
            PublicRepo = publicRepo;
            PullRequestId = pullRequestId;
            PullRequestTitle = pullRequestTitle;
            PullRequestUrl = pullRequestUrl;
            ReadOnly = readOnly;
            Repo = repo;
            RepoId = repoId;
            Repository = repository;
            RepositoryPublic = repositoryPublic;
            TargetLogin = targetLogin;
            Team = team;
            Timestamp = timestamp;
            TransportProtocol = transportProtocol;
            TransportProtocolName = transportProtocolName;
            User = user;
            UserAgent = userAgent;
            UserId = userId;
            Visibility = visibility;
        }

        public string Action { get; private set; }
        public bool? Active { get; private set; }
        public bool? ActiveWas { get; private set; }
        public string Actor { get; private set; }
        public long? ActorId { get; private set; }
        public string ActorIp { get; private set; }
        public object ActorLocation { get; private set; }
        public string BlockedUser { get; private set; }
        public string Business { get; private set; }
        public long? BusinessId { get; private set; }
        public object Config { get; private set; }
        public object ConfigWas { get; private set; }
        public string ContentType { get; private set; }
        public long CreatedAt { get; private set; }
        public object Data { get; private set; }
        public string DeployKeyFingerprint { get; private set; }
        public string DocumentId { get; private set; }
        public string Emoji { get; private set; }
        public object Events { get; private set; }
        public object EventsWere { get; private set; }
        public string Explanation { get; private set; }
        public string Fingerprint { get; private set; }
        public string HashedToken { get; private set; }
        public long? HookId { get; private set; }
        public bool? LimitedAvailability { get; private set; }
        public string Message { get; private set; }
        public string Name { get; private set; }
        public string OldUser { get; private set; }
        public string OpensshPublicKey { get; private set; }
        public string OperationType { get; private set; }
        public IReadOnlyList<string> Org { get; private set; }
        public IReadOnlyList<long> OrgId { get; private set; }
        public string PreviousVisibility { get; private set; }
        public bool? PublicRepo { get; private set; }
        public long? PullRequestId { get; private set; }
        public string PullRequestTitle { get; private set; }
        public string PullRequestUrl { get; private set; }
        public bool? ReadOnly { get; private set; }
        public string Repo { get; private set; }
        public long? RepoId { get; private set; }
        public string Repository { get; private set; }
        public bool? RepositoryPublic { get; private set; }
        public string TargetLogin { get; private set; }
        public string Team { get; private set; }
        public long Timestamp { get; private set; }
        public long? TransportProtocol { get; private set; }
        public string TransportProtocolName { get; private set; }
        public string User { get; private set; }
        public string UserAgent { get; private set; }
        public long? UserId { get; private set; }
        public string Visibility { get; private set; } 

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Action: {0}, Actor: {1}, DocumentID: {2}", Action, Actor, DocumentId);
            }
        }
    }
}



