using System;
using Burr.Helpers;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace Burr.Tests.Helpers
{
    public class EnsureTests
    {
        public class TheArgumentNotNullMethod
        {
            [Fact]
            public void ThrowsForNullArgument()
            {
                var res = Assert.Throws<ArgumentNullException>(() => Ensure.ArgumentNotNull(null, "arg"));
                res.ParamName.Should().Be("arg");
            }

            [Fact]
            public void DoesNotThrowForValidArgument()
            {
                Assert.DoesNotThrow(() => Ensure.ArgumentNotNull(new object(), "arg"));
            }
        }

        public class TheArgumentNotNullOrEmptyStringMethod
        {
            [Fact]
            public void ThrowsForNullArgument()
            {
                var res = Assert.Throws<ArgumentNullException>(() => Ensure.ArgumentNotNullOrEmptyString(null, "arg"));
                res.ParamName.Should().Be("arg");
            }

            [Theory]
            [InlineData("")]
            [InlineData(" ")]
            public void ThrowsForEmptyOrBlank(string data)
            {
                var res = Assert.Throws<ArgumentException>(() => Ensure.ArgumentNotNullOrEmptyString(data, "arg"));
                res.ParamName.Should().Be("arg");
            }

            [Fact]
            public void DoesNotThrowForValidArgument()
            {
                Assert.DoesNotThrow(() => Ensure.ArgumentNotNullOrEmptyString("a", "arg"));
            }
        }
    }
}
