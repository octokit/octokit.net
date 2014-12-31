using System;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class ApiErrorDetail
    {
        public string Message { get; protected set; }

        public string Code { get; protected set; }

        public string Field { get; protected set; }

        public string Resource { get; protected set; }
    }
}