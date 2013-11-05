using Octokit.Internal;

namespace Octokit
{
    public class PullRequestRequest : RequestParameters
    {
        public PullRequestRequest()
        {
            State = ItemState.Open;
        }

        public ItemState State { get; set; }

        [Parameter(Key = "head")]
        public string Head { get; set; }

        [Parameter(Key = "base")]
        public string Base { get; set; }
    }
}
