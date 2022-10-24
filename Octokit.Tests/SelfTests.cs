﻿using System.Reflection;
using Xunit;

/// <summary>
/// Tests to make sure our tests are ok.
/// </summary>
public class SelfTests
{
    [Fact]
    public void NoTestsUseAsyncVoid()
    {
        var errors = typeof(SelfTests).GetTypeInfo().Assembly.GetAsyncVoidMethodsList();
        Assert.Equal("", errors);
    }
}
