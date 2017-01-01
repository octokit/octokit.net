#if HAS_TYPEINFO
using System.Reflection;
#endif
using Xunit;

/// <summary>
/// Tests to make sure our tests are ok.
/// </summary>
public class SelfTests
{
    [Fact]
    public void NoTestsUseAsyncVoid()
    {
#if HAS_TYPEINFO
        var errors = typeof(SelfTests).GetTypeInfo().Assembly.GetAsyncVoidMethodsList();
#else
        var errors = typeof(SelfTests).Assembly.GetAsyncVoidMethodsList();
#endif
        Assert.Equal("", errors);
    }
}
