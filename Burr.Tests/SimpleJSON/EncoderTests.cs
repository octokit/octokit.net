using System;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleJSON;

namespace Tests.SimpleJSON {
    public enum LongEnumType :long {
        First,
        Second
    }

    [TestFixture]
    public class EncoderTests {
        private Dictionary<string, string> _stringTestCases;

        [SetUp]
        public void SetUp() {
            _stringTestCases = new Dictionary<string, string>
                                   {
                                       { "string", "\"string\"" },
                                       { "\" \\ \b \f \n \r \t", "\"\\\" \\\\ \\b \\f \\n \\r \\t\"" },
                                       { "\u03A0", "\"\\u03A0\"" },
                                       { "\0", "\"\\u0000\"" },
                                       { "\U0001d120", "\"\\uD834\\uDD20\"" }
                                   };
        }

        [Test]
        public void String() {
            foreach (var pair in _stringTestCases) {
                Assert.AreEqual(pair.Value, EncodeObject(pair.Key));
                Assert.AreEqual(pair.Value, EncodeObject(JObject.CreateString(pair.Key)));
            }
        }

        [Test]
        public void Int() {
            Assert.AreEqual("123", EncodeObject(123));
            Assert.AreEqual("2147483647", EncodeObject(Int32.MaxValue));
            Assert.AreEqual("-2147483648", EncodeObject(Int32.MinValue));

            Assert.AreEqual("123", EncodeObject(JObject.CreateNumber("123", "", "")));
            Assert.AreEqual("2147483647", EncodeObject(JObject.CreateNumber("2147483647", "", "")));
            Assert.AreEqual("-2147483648", EncodeObject(JObject.CreateNumber("-2147483648", "", "")));
        }

        [Test]
        public void UInt() {
            Assert.AreEqual("123", EncodeObject(123U));
            Assert.AreEqual("4294967295", EncodeObject(UInt32.MaxValue));

            Assert.AreEqual("4294967295", EncodeObject(JObject.CreateNumber("4294967295", "", "")));
        }

        [Test]
        public void Long() {
            Assert.AreEqual("123", EncodeObject(123L));
            Assert.AreEqual("9223372036854775807", EncodeObject(Int64.MaxValue));
            Assert.AreEqual("-9223372036854775808", EncodeObject(Int64.MinValue));

            Assert.AreEqual("9223372036854775807",
                            EncodeObject(JObject.CreateNumber("9223372036854775807", "", "")));
            Assert.AreEqual("-9223372036854775808",
                            EncodeObject(JObject.CreateNumber("-9223372036854775808", "", "")));
        }

        [Test]
        public void ULong() {
            Assert.AreEqual("123", EncodeObject(123UL));
            Assert.AreEqual("18446744073709551615", EncodeObject(UInt64.MaxValue));

            Assert.AreEqual("18446744073709551615",
                            EncodeObject(JObject.CreateNumber("18446744073709551615", "", "")));
        }

        [Test]
        public void Double() {
            Assert.AreEqual("1.5", EncodeObject(1.5));
            Assert.AreEqual("1000000", EncodeObject(1.0e6));
            Assert.AreEqual("-1000000", EncodeObject(-1.0e6));
            Assert.AreEqual("5E-06", EncodeObject(5.0e-6));

            Assert.AreEqual("1.5", EncodeObject(JObject.CreateNumber("1", ".5", "")));
            Assert.AreEqual("1000000", EncodeObject(JObject.CreateNumber("1000000", "", "")));
            Assert.AreEqual("-1000000", EncodeObject(JObject.CreateNumber("-1000000", "", "")));
            Assert.AreEqual("5E-06", EncodeObject(JObject.CreateNumber("5", "", "e-06")));
        }

        [Test]
        public void Float() {
            Assert.AreEqual("1.5", EncodeObject(1.5f));
            Assert.AreEqual("1000000", EncodeObject(1.0e6f));
            Assert.AreEqual("-1000000", EncodeObject(-1.0e6f));
            Assert.AreEqual("5E-05", EncodeObject(5.0e-5f));
        }

        [Test]
        public void Null() {
            Assert.AreEqual("null", EncodeObject(null));
            Assert.AreEqual("null", EncodeObject(JObject.CreateNull()));
        }

        [Test]
        public void Boolean() {
            Assert.AreEqual("true", EncodeObject(true));
            Assert.AreEqual("false", EncodeObject(false));

            Assert.AreEqual("true", EncodeObject(JObject.CreateBoolean(true)));
            Assert.AreEqual("false", EncodeObject(JObject.CreateBoolean(false)));
        }

        [Test]
        public void Array() {
            Assert.AreEqual("[1,2,3]", EncodeObject(new[] { 1, 2, 3 }));
            Assert.AreEqual("[[],\"str\",1.5]", EncodeObject(new object[] { new object[0], "str", 1.5 }));

            Assert.AreEqual("[1,2,3]",
                            EncodeObject(JObject.CreateArray(new List<JObject> {
                                                                     JObject.CreateNumber("1", "", ""),
                                                                     JObject.CreateNumber("2", "", ""),
                                                                     JObject.CreateNumber("3", "", "")
                                                                 })));
            Assert.AreEqual("[[],\"str\",1.5]",
                            EncodeObject(JObject.CreateArray(new List<JObject> {
                                                                     JObject.CreateArray(new List<JObject>()),
                                                                     JObject.CreateString("str"),
                                                                     JObject.CreateNumber("1", ".5", "")
                                                                 })));
        }

        [Test]
        public void Dictionary() {
            Assert.AreEqual("{\"X\":10,\"Y\":20}",
                            EncodeObject(new Dictionary<string, float> { { "X", 10 }, { "Y", 20 } }));

            Assert.AreEqual("{\"X\":10,\"Y\":20}",
                            EncodeObject(JObject.CreateObject(new Dictionary<string, JObject> {
                                                                      { "X", JObject.CreateNumber("10", "", "") },
                                                                      { "Y", JObject.CreateNumber("20", "", "") }
                                                                  })));
        }

        [Test]
        public void Enum() {
            Assert.AreEqual("0", EncodeObject(FloatSize.Double));
            Assert.AreEqual("1", EncodeObject(FloatSize.Single));
        }

        [Test]
        public void LongEnum() {
            Assert.AreEqual("0", EncodeObject(LongEnumType.First));
            Assert.AreEqual("1", EncodeObject(LongEnumType.Second));
        }

        private static string EncodeObject(object obj) {
            return JSONEncoder.Encode(obj);
        }
    }
}
