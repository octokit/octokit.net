﻿using System;
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
                Assert.Equal(expected, data.IsBlank());
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
                Assert.Equal(expected, data.IsNotBlank());
            }
        }

        public class TheToRubyCaseMethod
        {
            [Theory]
            [InlineData("Id", "id")]
            [InlineData("FirstName", "first_name")]
            public void ConvertsPascalToRuby(string source, string expected)
            {
                Assert.Equal(expected, source.ToRubyCase());
            }

            [Fact]
            public void EnsuresArgumentsNotNullOrEmpty()
            {
                string nullString = null;
                Assert.Throws<ArgumentNullException>(() => nullString.ToRubyCase());
                Assert.Throws<ArgumentException>(() => "".ToRubyCase());
            }
        }

        public class TheExpandUriTemplateMethod
        {
            [Theory]
            [InlineData("https://host.com/path?name=other", "https://host.com/path?name=other")]
            [InlineData("https://host.com/path?name=example name.txt", "https://host.com/path{?name}")]
            [InlineData("https://host.com/path", "https://host.com/path{?other}")]
            [InlineData("https://host.com/path?name=example name.txt&label=labeltext", "https://host.com/path{?name,label}")]
            [InlineData("https://host.com/path?name=example name.txt&label=labeltext", "https://host.com/path{?name,label,other}")]
            public void ExpandsUriTemplates(string expected, string template)
            {
                Assert.Equal(expected, template.ExpandUriTemplate(new { name = "example name.txt", label = "labeltext" }).ToString());
            }
        }

        public class EscapeDoubleQuotesMethod
        {
            [Fact]
            public void EscapeDoubleQuotesReturnsNullForNullInput()
            {
                Assert.Equal(null, (null as string).EscapeDoubleQuotes());
            }

            [Fact]
            public void EscapeDoubleQuotesReturnsInputWithoutDoubleQuotes()
            {
                string input = "some test input without double quotes in it";

                Assert.Equal(input, input.EscapeDoubleQuotes());
            }

            [Fact]
            public void EscapeDoubleQuotesEscapesAllDoubleQuotes()
            {
                string input = "\"test milestone\"";

                Assert.Equal("\\\"test milestone\\\"", input.EscapeDoubleQuotes());
            }
        }
    }
}
