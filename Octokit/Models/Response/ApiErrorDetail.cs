using System;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
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

        public string Message { get; protected set; }

        public string Code { get; protected set; }

        public string Field { get; protected set; }

        public string Resource { get; protected set; }
    }
}