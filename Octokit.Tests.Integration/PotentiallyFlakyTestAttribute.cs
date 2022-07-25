using System;

namespace Octokit.Tests.Integration
{
    /// <summary>
    /// A potentially flaky test could be:
    /// * Calls for details of repositories which already exist and was not created by the test itself (so could disappear over time)
    /// * Calls for details of users which already exist (so could disappear over time)
    /// </summary>
    public class PotentiallyFlakyTestAttribute : Attribute
    {
    }
}
