using System;
using System.Collections.Generic;
using System.Globalization;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class RequestParametersTests
    {
        public class TheToParametersDictionaryMethod
        {
            [Fact]
            public void CanConvertObjectToLowercaseDictionary()
            {
                var model = new SimpleRequestParameters { Bar = 123, Foo = "foovalue" };

                var result = model.ToParametersDictionary();

                Assert.Equal(2, result.Count);
                Assert.Equal("123", result["bar"]);
                Assert.Equal("foovalue", result["foo"]);
            }

            [Fact]
            public void OmitsNullValues()
            {
                var model = new SimpleRequestParameters { Bar = 123 };

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.DoesNotContain("foo", result.Keys);
                Assert.Equal("123", result["bar"]);
            }

            [Fact]
            public void HandlesEnums()
            {
                var model = new ClassWithEnum { Nom = Enomnomnom.Yuck };

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.Equal("yuck", result["nom"]);
            }

            [Fact]
            public void TreatsFirstEnumMemberAsDefault()
            {
                var model = new ClassWithEnum();

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.Equal("bland", result["nom"]);
            }

            [Fact]
            public void HandlesEnumsWithSpecifiedValue()
            {
                var model = new ClassWithEnum { Nom = Enomnomnom.NomNomNom };

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.Equal("noms", result["nom"]);
            }

            [Fact]
            public void ConvertsDatetTimeOffsetToIso8601Format()
            {
                var model = new WithDateTime
                {
                    When = DateTimeOffset.ParseExact("Wed 23 Jan 2013 8:30 AM -08:00",
                    "ddd dd MMM yyyy h:mm tt zzz", CultureInfo.InvariantCulture)
                };

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.Equal("2013-01-23T16:30:00Z", result["when"]);
            }

            [Fact]
            public void OmitsNullDatetTimeOffsetToIso8601Format()
            {
                var model = new WithDateTime();

                var result = model.ToParametersDictionary();

                Assert.Equal(0, result.Count);
            }

            [Fact]
            public void JoinsStringCollectionIntoCommaSeparatedString()
            {
                var model = new ClassWithStringCollection { Strings = new List<string> { "one", "two" } };

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.Equal("one,two", result["strings"]);
            }

            [Fact]
            public void DoesNotIncludeEmptyList()
            {
                var model = new ClassWithStringCollection { Strings = new List<string>() };

                var result = model.ToParametersDictionary();

                Assert.Equal(0, result.Count);
            }

            [Fact]
            public void UsesParameterAttributeForKey()
            {
                var model = new WithPropertyNameDifferentFromKey() { LongPropertyName = "verbose" };

                var result = model.ToParametersDictionary();

                Assert.Equal(1, result.Count);
                Assert.Equal("verbose", result["prop"]);
            }

            public class SimpleRequestParameters : RequestParameters
            {
                public string Foo { get; set; }
                public int Bar { get; set; }
            }

            public class ClassWithEnum : RequestParameters
            {
                public Enomnomnom Nom { get; set; }
            }

            public class WithDateTime : RequestParameters
            {
                public DateTimeOffset? When { get; set; }
            }

            public class ClassWithStringCollection : RequestParameters
            {
                public ICollection<string> Strings { get; set; }
            }

            public class WithPropertyNameDifferentFromKey : RequestParameters
            {
                [Parameter(Key = "prop")]
                public string LongPropertyName { get; set; }
            }

            public enum Enomnomnom
            {
                Bland,
                Delicious,
                Yuck,

                [Parameter(Value = "noms")]
                NomNomNom
            }
        }
    }
}
