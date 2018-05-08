namespace Octokit
{
    public class CheckRunImage
    {
        public CheckRunImage()
        {
        }

        public CheckRunImage(string alt, string imageUrl, string caption)
        {
            Alt = alt;
            ImageUrl = imageUrl;
            Caption = caption;
        }

        public string Alt { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Caption { get; protected set; }
    }
}