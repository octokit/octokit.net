namespace Octokit
{
    public class Organization : Account
    {
        /// <summary>
        /// The billing address for an organization. This is only returned when updating 
        /// an organization.
        /// </summary>
        public string BillingAddress { get; set; }
    }
}