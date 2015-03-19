namespace Octokit.Models.Request
{
    public class ApiOptions
    {
        public static readonly ApiOptions None = new ApiOptions();

        public int? StartPage { get; set; }
        public int? PageCount { get; set; }
        public int? PageSize { get; set; }
        public string Accepts { get; set; }
    }

}
