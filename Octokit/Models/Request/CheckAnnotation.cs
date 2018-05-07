using Octokit.Internal;

namespace Octokit
{
    public sealed class CheckAnnotation
    {
        string Filename { get; set; }
        string BlobHref { get; set; }
        int StartLine { get; set; }
        int EndLine { get; set; }
        CheckWarningLevel WarningLevel { get; set; }
        string Message { get; set; }
        string Title { get; set; }
        string RawDetails { get; set; }
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