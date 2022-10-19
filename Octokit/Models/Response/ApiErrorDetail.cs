using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [Serializable]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ApiErrorDetail
    {
        public ApiErrorDetail() { }

        public ApiErrorDetail(string message, string code, string field, string resource)
        {
            Message = message;
            Code = code;
            Field = field;
            Resource = resource;
        }

        public string Message { get; private set; }

        public string Code { get; private set; }

        public string Field { get; private set; }

        public string Resource { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0}, Code: {1}, Field: {2}", Message, Code, Field);
            }
        }
    }
}
