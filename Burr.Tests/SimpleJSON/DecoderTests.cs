using System.Collections.Generic;
using NUnit.Framework;
using SimpleJSON;

namespace Tests.SimpleJSON {
    [TestFixture]
    public class DecoderTests {
        private Dictionary<string, string> _stringTestCases;

        [SetUp]
        public void SetUp() {
            _stringTestCases = new Dictionary<string, string>
                                   {
                                       { "\"string\"", "string" },
                                       { "\"\\\" \\\\ \\b \\f \\n \\r \\t\"", "\" \\ \b \f \n \r \t" },
                                       { "\"\\u03A0\"", "\u03A0" },
                                       { "\"\u0000\"", "\0" },
                                       { "\"\\uD834\\uDD20\"", "\U0001d120" },
                                       { "\"\"", "" }
                                   };
        }

        [Test]
        public void String() {
            foreach (var pair in _stringTestCases) {
                Assert.AreEqual(pair.Value, (string)DecodeJSON(pair.Key));
            }
        }

        [Test]
        public void StringWithWhitespace() {
            foreach (var pair in _stringTestCases) {
                Assert.AreEqual(pair.Value, (string)DecodeJSON(" " + pair.Key + " "));
            }
        }

        [Test]
        public void Whitespaces() {
            Assert.AreEqual(123, (int)DecodeJSON(" 123"));
            Assert.AreEqual(123, (int)DecodeJSON("\t123"));
            Assert.AreEqual(123, (int)DecodeJSON("\n123"));
            Assert.AreEqual(123, (int)DecodeJSON("\r123"));
        }

        [Test]
        public void NumberZero() {
            var obj = DecodeJSON("0");

            Assert.AreEqual(JObjectKind.Number, obj.Kind);
            Assert.AreEqual(0, (int)obj);
        }

        [Test]
        public void SByteAndLarger() {
            var obj = DecodeJSON("123");
            var negObj = DecodeJSON("-123");

            Assert.IsFalse(obj.IsFractional);
            Assert.IsFalse(obj.IsNegative);
            Assert.IsFalse(negObj.IsFractional);
            Assert.IsTrue(negObj.IsNegative);
            Assert.AreEqual(IntegerSize.Int8, obj.MinInteger);
            Assert.AreEqual(IntegerSize.Int8, negObj.MinInteger);
            Assert.AreEqual(FloatSize.Single, obj.MinFloat);
            Assert.AreEqual(FloatSize.Single, negObj.MinFloat);

            Assert.AreEqual(123, (sbyte)obj);
            Assert.AreEqual(-123, (sbyte)negObj);

            Assert.AreEqual(123, (byte)obj);

            Assert.AreEqual(123, (short)obj);
            Assert.AreEqual(-123, (short)negObj);

            Assert.AreEqual(123, (ushort)obj);

            Assert.AreEqual(123, (int)obj);
            Assert.AreEqual(-123, (int)negObj);

            Assert.AreEqual(123, (uint)obj);

            Assert.AreEqual(123, (long)obj);
            Assert.AreEqual(-123, (long)negObj);

            Assert.AreEqual(123, (ulong)obj);

            Assert.AreEqual(123, (float)obj);
            Assert.AreEqual(-123, (float)negObj);

            Assert.AreEqual(123, (double)obj);
            Assert.AreEqual(-123, (double)negObj);
        }

        [Test]
        public void IntAndLarger() {
            var obj = DecodeJSON("1000000");
            var negObj = DecodeJSON("-1000000");

            Assert.IsFalse(obj.IsFractional);
            Assert.IsFalse(obj.IsNegative);
            Assert.IsFalse(negObj.IsFractional);
            Assert.IsTrue(negObj.IsNegative);

            Assert.AreEqual(IntegerSize.Int32, obj.MinInteger);
            Assert.AreEqual(IntegerSize.Int32, negObj.MinInteger);

            Assert.AreEqual(1000000, (int)obj);
            Assert.AreEqual(-1000000, (int)negObj);
        }

        [Test]
        public void SingleAndLarger() {
            var obj = DecodeJSON("150.5");
            var negObj = DecodeJSON("-150.5");

            Assert.IsTrue(obj.IsFractional);
            Assert.IsFalse(obj.IsNegative);
            Assert.IsTrue(negObj.IsFractional);
            Assert.IsTrue(negObj.IsNegative);
            Assert.AreEqual(FloatSize.Single, obj.MinFloat);
            Assert.AreEqual(FloatSize.Single, negObj.MinFloat);

            Assert.AreEqual(150.5, (float)obj);
            Assert.AreEqual(-150.5, (float)negObj);

            Assert.AreEqual(150.5, (double)obj);
            Assert.AreEqual(-150.5, (double)negObj);
        }

        [Test]
        public void Null() {
            Assert.AreEqual(JObjectKind.Null, DecodeJSON("null").Kind);
        }

        [Test]
        public void NullWithWhitespace() {
            Assert.AreEqual(JObjectKind.Null, DecodeJSON(" null ").Kind);
        }

        [Test]
        public void Boolean() {
            Assert.IsTrue((bool)DecodeJSON("true"));
            Assert.IsFalse((bool)DecodeJSON("false"));
        }

        [Test]
        public void BooleanWithWhitespace() {
            Assert.IsTrue((bool)DecodeJSON(" true "));
            Assert.IsFalse((bool)DecodeJSON(" false "));
        }

        [Test]
        public void Array() {
            var obj = DecodeJSON("[1,\"str\",null]");

            Assert.AreEqual(JObjectKind.Array, obj.Kind);
            Assert.AreEqual(3, obj.Count);
            Assert.AreEqual(1, (int)obj[0]);
            Assert.AreEqual("str", (string)obj[1]);
            Assert.AreEqual(JObjectKind.Null, obj[2].Kind);
        }

        [Test]
        public void ArrayWithWhitespace() {
            var obj = DecodeJSON(" [ 1 , \"str\" , null ] ");

            Assert.AreEqual(JObjectKind.Array, obj.Kind);
            Assert.AreEqual(3, obj.Count);
            Assert.AreEqual(1, (int)obj[0]);
            Assert.AreEqual("str", (string)obj[1]);
            Assert.AreEqual(JObjectKind.Null, obj[2].Kind);
        }

        [Test]
        public void EmptyArray() {
            var obj = DecodeJSON("[]");

            Assert.AreEqual(JObjectKind.Array, obj.Kind);
            Assert.AreEqual(0, obj.Count);
        }

        [Test]
        public void EmptyArrayWithWhitespace() {
            var obj = DecodeJSON(" [ ] ");

            Assert.AreEqual(JObjectKind.Array, obj.Kind);
            Assert.AreEqual(0, obj.Count);
        }

        [Test]
        public void NestedArrays() {
            var obj = DecodeJSON("[1,[2,3],4]");

            Assert.AreEqual(3, obj.Count);
            Assert.AreEqual(2, obj[1].Count);
            Assert.AreEqual(1, (int)obj[0]);
            Assert.AreEqual(2, (int)obj[1][0]);
            Assert.AreEqual(3, (int)obj[1][1]);
            Assert.AreEqual(4, (int)obj[2]);
        }

        [Test]
        public void NestedArraysWithWhitespace() {
            var obj = DecodeJSON(" [ 1 , [ 2 , 3 ] , 4 ] ");

            Assert.AreEqual(3, obj.Count);
            Assert.AreEqual(2, obj[1].Count);
            Assert.AreEqual(1, (int)obj[0]);
            Assert.AreEqual(2, (int)obj[1][0]);
            Assert.AreEqual(3, (int)obj[1][1]);
            Assert.AreEqual(4, (int)obj[2]);
        }

        [Test]
        public void NestedEmptyArray() {
            var obj = DecodeJSON("[[]]");

            Assert.AreEqual(1, obj.Count);
            Assert.AreEqual(0, obj[0].Count);
        }

        [Test]
        public void NestedEmptyArrayWithWhitespace() {
            var obj = DecodeJSON(" [ [ ] ] ");

            Assert.AreEqual(1, obj.Count);
            Assert.AreEqual(0, obj[0].Count);
        }

        [Test]
        public void EmptyObject() {
            var obj = DecodeJSON("{}");

            Assert.AreEqual(0, obj.Count);
            Assert.AreEqual(JObjectKind.Object, obj.Kind);
        }

        [Test]
        public void EmptyObjectWithWhitespace() {
            var obj = DecodeJSON(" { } ");

            Assert.AreEqual(0, obj.Count);
            Assert.AreEqual(JObjectKind.Object, obj.Kind);
        }

        [Test]
        public void NestedEmptyObject() {
            var obj = DecodeJSON("{\"key\":{}}");

            Assert.AreEqual(1, obj.Count);
            Assert.AreEqual(0, obj["key"].Count);
        }

        [Test]
        public void NestedEmptyObjectWithWhitespace() {
            var obj = DecodeJSON(" { \"key\" : { } } ");

            Assert.AreEqual(1, obj.Count);
            Assert.AreEqual(0, obj["key"].Count);
        }

        [Test]
        public void Object() {
            var obj = DecodeJSON("{\"key1\":12,\"key2\":\"value\"}");

            Assert.AreEqual(2, obj.Count);
            Assert.AreEqual(JObjectKind.Object, obj.Kind);
            Assert.AreEqual(12, (int)obj["key1"]);
            Assert.AreEqual("value", (string)obj["key2"]);
        }

        [Test]
        public void ObjectWithWhitespace() {
            var obj = DecodeJSON(" { \"key1\" : 12 , \"key2\" : \"value\" } ");

            Assert.AreEqual(2, obj.Count);
            Assert.AreEqual(JObjectKind.Object, obj.Kind);
            Assert.AreEqual(12, (int)obj["key1"]);
            Assert.AreEqual("value", (string)obj["key2"]);
        }

        private static JObject DecodeJSON(string json) {
            return JSONDecoder.Decode(json);
        }
    }
}