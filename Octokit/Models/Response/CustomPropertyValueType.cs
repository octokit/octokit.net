using Octokit.Internal;

namespace Octokit
{
    public enum CustomPropertyValueType
    {
        [Parameter(Value = "string")]
        String,
        [Parameter(Value = "single_select")]
        SingleSelect,
        [Parameter(Value = "multi_select")]
        MultiSelect,
        [Parameter(Value = "true_false")]
        TrueFalse,
    }
}
