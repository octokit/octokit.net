namespace Octokit
{
    /// <summary>
    /// Describes a new label to create via the <see cref="IIssuesLabelsClient.Create(string,string,NewLabel)"/> method.
    /// </summary>
    public class NewLabel
    {
        public NewLabel(string name, string color)
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
