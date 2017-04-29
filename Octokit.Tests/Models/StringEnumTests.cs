using System;
using Xunit;

namespace Octokit.Tests.Models
{
    public class StringEnumTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetValue()
            {
                var stringEnum = new StringEnum<AccountType>("user");

                Assert.Equal("user", stringEnum.Value);
            }

            [Fact]
            public void ShouldSetValueAndParsedValue()
            {
                var stringEnum = new StringEnum<AccountType>(AccountType.Bot);

                Assert.Equal("bot", stringEnum.Value);
                Assert.Equal(AccountType.Bot, stringEnum.ParsedValue);
            }

            [Fact]
            public void ShouldThrowForInvalidEnumValue()
            {
                Assert.Throws<ArgumentException>(() => new StringEnum<AccountType>((AccountType) 1337));
            }
        }

        public class TheParsedValueProperty
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("Cow")]
            public void ShouldThrowForInvalidValue(string value)
            {
                var stringEnum = new StringEnum<AccountType>(value);

                Assert.Throws<ArgumentException>(() => stringEnum.ParsedValue);
            }
        }

        public class TheTryParseMethod
        {
            [Fact]
            public void ShouldReturnTrueForValidValue()
            {
                var stringEnum = new StringEnum<AccountType>("Bot");

                AccountType type;
                var result = stringEnum.TryParse(out type);

                Assert.True(result);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("Cow")]
            public void ShouldReturnFalseForInvalidValue(string value)
            {
                var stringEnum = new StringEnum<AccountType>(value);

                AccountType type;
                var result = stringEnum.TryParse(out type);

                Assert.False(result);
            }
        }

        public class TheImplicitConversionOperator
        {
            [Fact]
            public void ShouldSetValue()
            {
                StringEnum<AccountType> stringEnum = "user";

                Assert.Equal("user", stringEnum.Value);
            }

            [Fact]
            public void ShouldSetValueAndParsedValue()
            {
                StringEnum<AccountType> stringEnum = AccountType.Bot;

                Assert.Equal("bot", stringEnum.Value);
                Assert.Equal(AccountType.Bot, stringEnum.ParsedValue);
            }

            [Fact]
            public void ShouldThrowForInvalidEnumValue()
            {
                StringEnum<AccountType> stringEnum;
                Assert.Throws<ArgumentException>(() => stringEnum = (AccountType) 1337);
            }
        }

        public class TheEqualityOperator
        {
            [Fact]
            public void IsCaseInsensitive()
            {
                var first = new StringEnum<AccountType>("bot");
                var second = new StringEnum<AccountType>("BoT");

                Assert.True(first == second);
            }
        }
    }
}
