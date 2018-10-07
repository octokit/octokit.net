using System;
using Xunit;

namespace Octokit.Tests.Models
{
    public class StringEnumTests
    {
        public class TheCtor
        {
            [Fact]
            public void ShouldThrowForNullOrEmptyStringValues()
            {
                Assert.Throws<ArgumentNullException>(() => new StringEnum<AccountType>(null));

                Assert.Throws<ArgumentException>(() => new StringEnum<AccountType>(""));
            }

            [Fact]
            public void ShouldSetValue()
            {
                var stringEnum = new StringEnum<AccountType>("user");

                Assert.Equal("user", stringEnum.StringValue);
            }

            [Fact]
            public void ShouldSetValueAndParsedValue()
            {
                var stringEnum = new StringEnum<AccountType>(AccountType.Bot);

                Assert.Equal("Bot", stringEnum.StringValue);
                Assert.Equal(AccountType.Bot, stringEnum.Value);
                Assert.Equal("Bot", stringEnum);
                Assert.Equal(AccountType.Bot, stringEnum);
            }

            [Fact]
            public void ShouldRespectCustomPropertyAttributes()
            {
                StringEnum<ReactionType> stringEnum = ReactionType.Plus1;

                Assert.Equal("+1", stringEnum.StringValue);
                Assert.Equal(ReactionType.Plus1, stringEnum.Value);
            }

            [Fact]
            public void ShouldThrowForInvalidEnumValue()
            {
                Assert.Throws<ArgumentException>(() => new StringEnum<AccountType>((AccountType)1337));
            }
        }

        public class TheValueProperty
        {
            [Fact]
            public void ShouldThrowForInvalidValue()
            {
                var stringEnum = new StringEnum<AccountType>("Cow");
                Assert.Throws<ArgumentException>(() => stringEnum.Value);
            }

            [Fact]
            public void ShouldHandleUnderscores()
            {
                var stringEnum = new StringEnum<EventInfoState>("review_dismissed");

                Assert.Equal("review_dismissed", stringEnum.StringValue);
                Assert.Equal(EventInfoState.ReviewDismissed, stringEnum.Value);
            }

            [Fact]
            public void ShouldHandleHyphens()
            {
                var stringEnum = new StringEnum<EncodingType>("utf-8");

                Assert.Equal("utf-8", stringEnum.StringValue);
                Assert.Equal(EncodingType.Utf8, stringEnum.Value);
            }

            [Fact]
            public void ShouldHandleCustomPropertyAttribute()
            {
                var stringEnum = new StringEnum<ReactionType>("+1");

                Assert.Equal("+1", stringEnum.StringValue);
                Assert.Equal(ReactionType.Plus1, stringEnum.Value);
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

            [Fact]
            public void ShouldReturnFalseForInvalidValue()
            {
                var stringEnum = new StringEnum<AccountType>("Cow");

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

                Assert.Equal("user", stringEnum.StringValue);
            }

            [Fact]
            public void ShouldSetValueAndParsedValue()
            {
                StringEnum<AccountType> stringEnum = AccountType.Bot;

                Assert.Equal("Bot", stringEnum.StringValue);
                Assert.Equal(AccountType.Bot, stringEnum.Value);
            }

            [Fact]
            public void ShouldThrowForInvalidEnumValue()
            {
                StringEnum<AccountType> stringEnum;
                Assert.Throws<ArgumentException>(() => stringEnum = (AccountType)1337);
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

            [Fact]
            public void FallsBackToStringComparison()
            {
                var first = new StringEnum<AccountType>("god");
                var second = new StringEnum<AccountType>("GoD");

                Assert.True(first == second);
            }
        }
    }
}
