namespace Nocto.Http
{
    public class GitHubResponse<T> : Response<T>
    {
        public ApiInfo ApiInfo { get; set; }
    }
}
