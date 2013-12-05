namespace Octokit
{
    public class LabelUpdate
    {
        public LabelUpdate(string name, string color)
        {
            Name = name;
            Color = color;
        }


        /// <summary>
        /// Name of the label (required)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of the label (required)
        /// </summary>
        public string Color { get; set; }
    }
}
