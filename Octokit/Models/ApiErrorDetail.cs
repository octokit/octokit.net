namespace Octokit
{
    public class ApiErrorDetail
    {
        public string Message { get; set; }

        public string Code { get; set; }

        public string Field { get; set; }

        public string Resource { get; set; }
    }
}