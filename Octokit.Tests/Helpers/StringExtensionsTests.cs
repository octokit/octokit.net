using System;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Helpers
{
    public class StringExtensionsTests
    {
        public class TheIsBlankMethod
        {
            [InlineData(null, true)]
            [InlineData("", true)]
            [InlineData(" ", true)]
            [InlineData("nope", false)]
            [Theory]
            public void ProperlyDetectsBlankStrings(string data, bool expected)
            {
                data.IsBlank().Should().Be(expected);
            }
        }

        public class TheIsNotBlankMethod
        {
            [InlineData(null, false)]
            [InlineData("", false)]
            [InlineData(" ", false)]
            [InlineData("nope", true)]
            [Theory]
            public void ProperlyDetectsBlankStrings(string data, bool expected)
            {
                data.IsNotBlank().Should().Be(expected);
            }
        }

        public class TheToRubyCaseMethod
        {
            [Theory]
            [InlineData("Id", "id")]
            [InlineData("FirstName", "first_name")]
            public void ConvertsPascalToRuby(string source, string expected)
            {
                source.ToRubyCase().Should().Be(expected);
            }

            [Fact]
            public void EnsuresArgumentsNotNullOrEmpty()
            {
                string nullString = null;
                Assert.Throws<ArgumentNullException>(() => nullString.ToRubyCase());
                Assert.Throws<ArgumentException>(() => "".ToRubyCase());
            }
        }
    }
}
