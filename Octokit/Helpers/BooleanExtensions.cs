namespace Octokit
{
    internal static class BooleanExtensions
    {
        public static bool IsNotNull(this object value)
        {
            return !(null == value);
        }
    }
}
