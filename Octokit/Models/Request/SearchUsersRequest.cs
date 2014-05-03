using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Searching Users
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchUsersRequest : BaseSearchRequest
    {
        public SearchUsersRequest(string term) : base(term)
        {
        }

        /// <summary>
        /// Optional Sort field. One of followers, repositories, or joined. If not provided (null), results are sorted by best match.
        /// <remarks>https://help.github.com/articles/searching-users#sorting</remarks>
        /// </summary>
        public UsersSearchSort? SortField { get; set; }

        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        /// <summary>
        /// Filter users based on the number of followers they have.
        /// <remarks>https://help.github.com/articles/searching-users#followers</remarks>       
        /// </summary>
        public Range Followers { get; set; }

        /// <summary>
        /// Filter users based on when they joined.
        /// <remarks>https://help.github.com/articles/searching-users#created</remarks>       
        /// </summary>
        public DateRange Created { get; set; }

        /// <summary>
        /// Filter users by the location indicated in their profile.
        /// <remarks>https://help.github.com/articles/searching-users#location</remarks>       
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Filters users based on the number of repositories they have.
        /// <remarks>https://help.github.com/articles/searching-users#repository-count</remarks>       
        /// </summary>
        public Range Repositories { get; set; }

        /// <summary>
        /// Search for users that have repositories that match a certain language.
        /// <remarks>https://help.github.com/articles/searching-users#language</remarks>       
        /// </summary>
        public Language? Language { get; set; }

        /// <summary>
        /// With this qualifier you can restrict the search to just personal accounts or just organization accounts.
        /// <remarks>https://help.github.com/articles/searching-users#type</remarks>       
        /// </summary>
        public AccountType? AccountType { get; set; }

        private IEnumerable<UserInQualifier> _inQualifier;

        /// <summary>
        /// Qualifies which fields are searched. With this qualifier you can restrict the search to just the username, public email, full name, or any combination of these.
        /// <remarks>https://help.github.com/articles/searching-users#search-in</remarks>       
        /// </summary>
        public IEnumerable<UserInQualifier> In
        {
            get
            {
                return _inQualifier;
            }
            set
            {
                if (value != null && value.Any())
                    _inQualifier = value.Distinct().ToList();
            }
        }

        public override IReadOnlyCollection<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (AccountType != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "type:{0}", AccountType));
            }

            if (In != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "in:{0}", String.Join(",", In)));
            }

            if (Repositories != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "repos:{0}", Repositories));
            }

            if (Location.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "location:{0}", Location));
            }

            if (Language != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "language:{0}", Language));
            }

            if (Created != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "created:{0}", Created));
            }

            if (Followers != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "followers:{0}", Followers));
            }

            return new ReadOnlyCollection<string>(parameters);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Term: {0} Sort: {1}", Term, Sort);
            }
        }
    }

    public enum AccountType
    {
        User,
        Org
    }

    public enum UserInQualifier
    {
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Username")]
        Username,
        Email,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fullname")]
        Fullname
    }

    public enum UsersSearchSort
    {
        Followers,
        Repositories,
        Joined
    }
}
