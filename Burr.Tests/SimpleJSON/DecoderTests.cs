using System.Collections.Generic;
using Xunit;
using Burr.SimpleJson;

namespace Tests.SimpleJSON
{
    public class DecoderTests
    {
        private Dictionary<string, string> _stringTestCases = new Dictionary<string, string>
                                   {
                                       { "\"string\"", "string" },
                                       { "\"\\\" \\\\ \\b \\f \\n \\r \\t\"", "\" \\ \b \f \n \r \t" },
                                       { "\"\\u03A0\"", "\u03A0" },
                                       { "\"\u0000\"", "\0" },
                                       { "\"\\uD834\\uDD20\"", "\U0001d120" },
                                       { "\"\"", "" }
                                   };

        [Fact]
        public void String()
        {
            foreach (var pair in _stringTestCases)
            {
                Assert.Equal(pair.Value, (string)DecodeJSON(pair.Key));
            }
        }

        [Fact]
        public void StringWithWhitespace()
        {
            foreach (var pair in _stringTestCases)
            {
                Assert.Equal(pair.Value, (string)DecodeJSON(" " + pair.Key + " "));
            }
        }

        [Fact]
        public void Whitespaces()
        {
            Assert.Equal(123, (int)DecodeJSON(" 123"));
            Assert.Equal(123, (int)DecodeJSON("\t123"));
            Assert.Equal(123, (int)DecodeJSON("\n123"));
            Assert.Equal(123, (int)DecodeJSON("\r123"));
        }

        [Fact]
        public void NumberZero()
        {
            var obj = DecodeJSON("0");

            Assert.Equal(JObjectKind.Number, obj.Kind);
            Assert.Equal(0, (int)obj);
        }

        [Fact]
        public void SByteAndLarger()
        {
            var obj = DecodeJSON("123");
            var negObj = DecodeJSON("-123");

            Assert.False(obj.IsFractional);
            Assert.False(obj.IsNegative);
            Assert.False(negObj.IsFractional);
            Assert.True(negObj.IsNegative);
            Assert.Equal(IntegerSize.Int8, obj.MinInteger);
            Assert.Equal(IntegerSize.Int8, negObj.MinInteger);
            Assert.Equal(FloatSize.Single, obj.MinFloat);
            Assert.Equal(FloatSize.Single, negObj.MinFloat);

            Assert.Equal(123, (sbyte)obj);
            Assert.Equal(-123, (sbyte)negObj);

            Assert.Equal(123, (byte)obj);

            Assert.Equal(123, (short)obj);
            Assert.Equal(-123, (short)negObj);

            Assert.Equal(123, (ushort)obj);

            Assert.Equal(123, (int)obj);
            Assert.Equal(-123, (int)negObj);

            Assert.Equal((uint)123, (uint)obj);

            Assert.Equal(123, (long)obj);
            Assert.Equal(-123, (long)negObj);

            Assert.Equal((ulong)123, (ulong)obj);

            Assert.Equal(123, (float)obj);
            Assert.Equal(-123, (float)negObj);

            Assert.Equal(123, (double)obj);
            Assert.Equal(-123, (double)negObj);
        }

        [Fact]
        public void IntAndLarger()
        {
            var obj = DecodeJSON("1000000");
            var negObj = DecodeJSON("-1000000");

            Assert.False(obj.IsFractional);
            Assert.False(obj.IsNegative);
            Assert.False(negObj.IsFractional);
            Assert.True(negObj.IsNegative);

            Assert.Equal(IntegerSize.Int32, obj.MinInteger);
            Assert.Equal(IntegerSize.Int32, negObj.MinInteger);

            Assert.Equal(1000000, (int)obj);
            Assert.Equal(-1000000, (int)negObj);
        }

        [Fact]
        public void SingleAndLarger()
        {
            var obj = DecodeJSON("150.5");
            var negObj = DecodeJSON("-150.5");

            Assert.True(obj.IsFractional);
            Assert.False(obj.IsNegative);
            Assert.True(negObj.IsFractional);
            Assert.True(negObj.IsNegative);
            Assert.Equal(FloatSize.Single, obj.MinFloat);
            Assert.Equal(FloatSize.Single, negObj.MinFloat);

            Assert.Equal(150.5, (float)obj);
            Assert.Equal(-150.5, (float)negObj);

            Assert.Equal(150.5, (double)obj);
            Assert.Equal(-150.5, (double)negObj);
        }

        [Fact]
        public void Null()
        {
            Assert.Equal(JObjectKind.Null, DecodeJSON("null").Kind);
        }

        [Fact]
        public void NullWithWhitespace()
        {
            Assert.Equal(JObjectKind.Null, DecodeJSON(" null ").Kind);
        }

        [Fact]
        public void Boolean()
        {
            Assert.True((bool)DecodeJSON("true"));
            Assert.False((bool)DecodeJSON("false"));
        }

        [Fact]
        public void BooleanWithWhitespace()
        {
            Assert.True((bool)DecodeJSON(" true "));
            Assert.False((bool)DecodeJSON(" false "));
        }

        [Fact]
        public void Array()
        {
            var obj = DecodeJSON("[1,\"str\",null]");

            Assert.Equal(JObjectKind.Array, obj.Kind);
            Assert.Equal(3, obj.Count);
            Assert.Equal(1, (int)obj[0]);
            Assert.Equal("str", (string)obj[1]);
            Assert.Equal(JObjectKind.Null, obj[2].Kind);
        }

        [Fact]
        public void ArrayWithWhitespace()
        {
            var obj = DecodeJSON(" [ 1 , \"str\" , null ] ");

            Assert.Equal(JObjectKind.Array, obj.Kind);
            Assert.Equal(3, obj.Count);
            Assert.Equal(1, (int)obj[0]);
            Assert.Equal("str", (string)obj[1]);
            Assert.Equal(JObjectKind.Null, obj[2].Kind);
        }

        [Fact]
        public void EmptyArray()
        {
            var obj = DecodeJSON("[]");

            Assert.Equal(JObjectKind.Array, obj.Kind);
            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void EmptyArrayWithWhitespace()
        {
            var obj = DecodeJSON(" [ ] ");

            Assert.Equal(JObjectKind.Array, obj.Kind);
            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void NestedArrays()
        {
            var obj = DecodeJSON("[1,[2,3],4]");

            Assert.Equal(3, obj.Count);
            Assert.Equal(2, obj[1].Count);
            Assert.Equal(1, (int)obj[0]);
            Assert.Equal(2, (int)obj[1][0]);
            Assert.Equal(3, (int)obj[1][1]);
            Assert.Equal(4, (int)obj[2]);
        }

        [Fact]
        public void NestedArraysWithWhitespace()
        {
            var obj = DecodeJSON(" [ 1 , [ 2 , 3 ] , 4 ] ");

            Assert.Equal(3, obj.Count);
            Assert.Equal(2, obj[1].Count);
            Assert.Equal(1, (int)obj[0]);
            Assert.Equal(2, (int)obj[1][0]);
            Assert.Equal(3, (int)obj[1][1]);
            Assert.Equal(4, (int)obj[2]);
        }

        [Fact]
        public void NestedEmptyArray()
        {
            var obj = DecodeJSON("[[]]");

            Assert.Equal(1, obj.Count);
            Assert.Equal(0, obj[0].Count);
        }

        [Fact]
        public void NestedEmptyArrayWithWhitespace()
        {
            var obj = DecodeJSON(" [ [ ] ] ");

            Assert.Equal(1, obj.Count);
            Assert.Equal(0, obj[0].Count);
        }

        [Fact]
        public void EmptyObject()
        {
            var obj = DecodeJSON("{}");

            Assert.Equal(0, obj.Count);
            Assert.Equal(JObjectKind.Object, obj.Kind);
        }

        [Fact]
        public void EmptyObjectWithWhitespace()
        {
            var obj = DecodeJSON(" { } ");

            Assert.Equal(0, obj.Count);
            Assert.Equal(JObjectKind.Object, obj.Kind);
        }

        [Fact]
        public void NestedEmptyObject()
        {
            var obj = DecodeJSON("{\"key\":{}}");

            Assert.Equal(1, obj.Count);
            Assert.Equal(0, obj["key"].Count);
        }

        [Fact]
        public void NestedEmptyObjectWithWhitespace()
        {
            var obj = DecodeJSON(" { \"key\" : { } } ");

            Assert.Equal(1, obj.Count);
            Assert.Equal(0, obj["key"].Count);
        }

        [Fact]
        public void Object()
        {
            var obj = DecodeJSON("{\"key1\":12,\"key2\":\"value\"}");

            Assert.Equal(2, obj.Count);
            Assert.Equal(JObjectKind.Object, obj.Kind);
            Assert.Equal(12, (int)obj["key1"]);
            Assert.Equal("value", (string)obj["key2"]);
        }

        [Fact]
        public void ObjectWithWhitespace()
        {
            var obj = DecodeJSON(" { \"key1\" : 12 , \"key2\" : \"value\" } ");

            Assert.Equal(2, obj.Count);
            Assert.Equal(JObjectKind.Object, obj.Kind);
            Assert.Equal(12, (int)obj["key1"]);
            Assert.Equal("value", (string)obj["key2"]);
        }

        private static JObject DecodeJSON(string json)
        {
            return JsonDecoder.Decode(json);
        }
    }
}