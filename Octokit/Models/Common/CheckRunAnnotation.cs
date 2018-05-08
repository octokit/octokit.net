using Octokit.Internal;

namespace Octokit
{
    public class CheckRunAnnotation
    {
        public CheckRunAnnotation(string filename, string blobHref, int startLine, int endLine, CheckWarningLevel warningLevel, string message, string title, string rawDetails)
        {
            Filename = filename;
            BlobHref = blobHref;
            StartLine = startLine;
            EndLine = endLine;
            WarningLevel = warningLevel;
            Message = message;
            Title = title;
            RawDetails = rawDetails;
        }

        public string Filename { get; protected set; }
        public string BlobHref { get; protected set; }
        public int StartLine { get; protected set; }
        public int EndLine { get; protected set; }
        public StringEnum<CheckWarningLevel> WarningLevel { get; protected set; }
        public string Message { get; protected set; }
        public string Title { get; protected set; }
        public string RawDetails { get; protected set; }
    }

    public enum CheckWarningLevel
    {
        [Parameter(Value = "notice")]
        Notice,

        [Parameter(Value = "warning")]
        Warning,

        [Parameter(Value = "failure")]
        Failure,
    }
}