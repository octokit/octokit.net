using System;
using System.Collections.Generic;
using Xunit;
using Burr.SimpleJSON;

namespace Tests.SimpleJSON
{
    public enum LongEnumType : long
    {
        First,
        Second
    }

    public class EncoderTests
    {
        private Dictionary<string, string> _stringTestCases = new Dictionary<string, string>
                                   {
                                       { "string", "\"string\"" },
                                       { "\" \\ \b \f \n \r \t", "\"\\\" \\\\ \\b \\f \\n \\r \\t\"" },
                                       { "\u03A0", "\"\\u03A0\"" },
                                       { "\0", "\"\\u0000\"" },
                                       { "\U0001d120", "\"\\uD834\\uDD20\"" }
                                   };

        [Fact]
        public void String()
        {
            foreach (var pair in _stringTestCases)
            {
                Assert.Equal(pair.Value, EncodeObject(pair.Key));
                Assert.Equal(pair.Value, EncodeObject(JObject.CreateString(pair.Key)));
            }
        }

        [Fact]
        public void Int()
        {
            Assert.Equal("123", EncodeObject(123));
            Assert.Equal("2147483647", EncodeObject(Int32.MaxValue));
            Assert.Equal("-2147483648", EncodeObject(Int32.MinValue));

            Assert.Equal("123", EncodeObject(JObject.CreateNumber("123", "", "")));
            Assert.Equal("2147483647", EncodeObject(JObject.CreateNumber("2147483647", "", "")));
            Assert.Equal("-2147483648", EncodeObject(JObject.CreateNumber("-2147483648", "", "")));
        }

        [Fact]
        public void UInt()
        {
            Assert.Equal("123", EncodeObject(123U));
            Assert.Equal("4294967295", EncodeObject(UInt32.MaxValue));

            Assert.Equal("4294967295", EncodeObject(JObject.CreateNumber("4294967295", "", "")));
        }

        [Fact]
        public void Long()
        {
            Assert.Equal("123", EncodeObject(123L));
            Assert.Equal("9223372036854775807", EncodeObject(Int64.MaxValue));
            Assert.Equal("-9223372036854775808", EncodeObject(Int64.MinValue));

            Assert.Equal("9223372036854775807",
                            EncodeObject(JObject.CreateNumber("9223372036854775807", "", "")));
            Assert.Equal("-9223372036854775808",
                            EncodeObject(JObject.CreateNumber("-9223372036854775808", "", "")));
        }

        [Fact]
        public void ULong()
        {
            Assert.Equal("123", EncodeObject(123UL));
            Assert.Equal("18446744073709551615", EncodeObject(UInt64.MaxValue));

            Assert.Equal("18446744073709551615",
                            EncodeObject(JObject.CreateNumber("18446744073709551615", "", "")));
        }

        [Fact]
        public void Double()
        {
            Assert.Equal("1.5", EncodeObject(1.5));
            Assert.Equal("1000000", EncodeObject(1.0e6));
            Assert.Equal("-1000000", EncodeObject(-1.0e6));
            Assert.Equal("5E-06", EncodeObject(5.0e-6));

            Assert.Equal("1.5", EncodeObject(JObject.CreateNumber("1", ".5", "")));
            Assert.Equal("1000000", EncodeObject(JObject.CreateNumber("1000000", "", "")));
            Assert.Equal("-1000000", EncodeObject(JObject.CreateNumber("-1000000", "", "")));
            Assert.Equal("5E-06", EncodeObject(JObject.CreateNumber("5", "", "e-06")));
        }

        [Fact]
        public void Float()
        {
            Assert.Equal("1.5", EncodeObject(1.5f));
            Assert.Equal("1000000", EncodeObject(1.0e6f));
            Assert.Equal("-1000000", EncodeObject(-1.0e6f));
            Assert.Equal("5E-05", EncodeObject(5.0e-5f));
        }

        [Fact]
        public void Null()
        {
            Assert.Equal("null", EncodeObject(null));
            Assert.Equal("null", EncodeObject(JObject.CreateNull()));
        }

        [Fact]
        public void Boolean()
        {
            Assert.Equal("true", EncodeObject(true));
            Assert.Equal("false", EncodeObject(false));

            Assert.Equal("true", EncodeObject(JObject.CreateBoolean(true)));
            Assert.Equal("false", EncodeObject(JObject.CreateBoolean(false)));
        }

        [Fact]
        public void Array()
        {
            Assert.Equal("[1,2,3]", EncodeObject(new[] { 1, 2, 3 }));
            Assert.Equal("[[],\"str\",1.5]", EncodeObject(new object[] { new object[0], "str", 1.5 }));

            Assert.Equal("[1,2,3]",
                            EncodeObject(JObject.CreateArray(new List<JObject> {
                                                                     JObject.CreateNumber("1", "", ""),
                                                                     JObject.CreateNumber("2", "", ""),
                                                                     JObject.CreateNumber("3", "", "")
                                                                 })));
            Assert.Equal("[[],\"str\",1.5]",
                            EncodeObject(JObject.CreateArray(new List<JObject> {
                                                                     JObject.CreateArray(new List<JObject>()),
                                                                     JObject.CreateString("str"),
                                                                     JObject.CreateNumber("1", ".5", "")
                                                                 })));
        }

        [Fact]
        public void Dictionary()
        {
            Assert.Equal("{\"X\":10,\"Y\":20}",
                            EncodeObject(new Dictionary<string, float> { { "X", 10 }, { "Y", 20 } }));

            Assert.Equal("{\"X\":10,\"Y\":20}",
                            EncodeObject(JObject.CreateObject(new Dictionary<string, JObject> {
                                                                      { "X", JObject.CreateNumber("10", "", "") },
                                                                      { "Y", JObject.CreateNumber("20", "", "") }
                                                                  })));
        }

        [Fact]
        public void Enum()
        {
            Assert.Equal("0", EncodeObject(FloatSize.Double));
            Assert.Equal("1", EncodeObject(FloatSize.Single));
        }

        [Fact]
        public void LongEnum()
        {
            Assert.Equal("0", EncodeObject(LongEnumType.First));
            Assert.Equal("1", EncodeObject(LongEnumType.Second));
        }

        private static string EncodeObject(object obj)
        {
            return JSONEncoder.Encode(obj);
        }
    }
}
