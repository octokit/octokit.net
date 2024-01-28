using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReactionSummary
    {
        public ReactionSummary() { }

        public ReactionSummary(int totalCount, int plus1, int minus1, int laugh, int confused, int heart, int hooray, int eyes, int rocket, string url)
        {
            TotalCount = totalCount;
            Plus1 = plus1;
            Minus1 = minus1;
            Laugh = laugh;
            Confused = confused;
            Heart = heart;
            Hooray = hooray;
            Url = url;
            Eyes = eyes;
            Rocket = rocket;
        }

        public int TotalCount { get; private set; }
        [Parameter(Key = "+1")]
        public int Plus1 { get; private set; }
        [Parameter(Key = "-1")]
        public int Minus1 { get; private set; }
        public int Laugh { get; private set; }
        public int Confused { get; private set; }
        public int Heart { get; private set; }
        public int Hooray { get; private set; }
        public int Eyes { get; private set; }
        public int Rocket { get; private set; }
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "TotalCount: {0} +1: {1} -1: {2} Laugh: {3} Confused: {4} Heart: {5} Hooray: {6} Eyes: {7} Rocket: {8}",
                    TotalCount,
                    Plus1,
                    Minus1,
                    Laugh,
                    Confused,
                    Heart,
                    Hooray, 
					Eyes,
					Rocket);
            }
        }
    }
}
