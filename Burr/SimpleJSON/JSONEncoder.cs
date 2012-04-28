using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Burr.SimpleJSON {
    public class JSONEncoder {
        public static string Encode(object obj) {
            var encoder = new JSONEncoder();
            encoder.EncodeObject(obj);
            return encoder._buffer.ToString();
        }

        private readonly StringBuilder _buffer = new StringBuilder();

        private static readonly Dictionary<char, string> EscapeChars =
            new Dictionary<char, string>
                {
                    { '"', "\\\"" },
                    { '\\', "\\\\" },
                    { '\b', "\\b" },
                    { '\f', "\\f" },
                    { '\n', "\\n" },
                    { '\r', "\\r" },
                    { '\t', "\\t" },
                    { '\u2028', "\\u2028" },
                    { '\u2029', "\\u2029" }
                };

        private JSONEncoder() { }

        private void EncodeObject(object obj) {
            if (obj == null) {
                EncodeNull();
            } else if (obj is string) {
                EncodeString((string)obj);
            } else if (obj is float) {
                EncodeFloat((float)obj);
            } else if (obj is double) {
                EncodeDouble((double)obj);
            } else if (obj is int) {
                EncodeLong((int)obj);
            } else if (obj is uint) {
                EncodeULong((uint)obj);
            } else if (obj is long) {
                EncodeLong((long)obj);
            } else if (obj is ulong) {
                EncodeULong((ulong)obj);
            } else if (obj is short) {
                EncodeLong((short)obj);
            } else if (obj is ushort) {
                EncodeULong((ushort)obj);
            } else if (obj is byte) {
                EncodeULong((byte)obj);
            } else if (obj is bool) {
                EncodeBool((bool)obj);
            } else if (obj is IDictionary) {
                EncodeDictionary((IDictionary)obj);
            } else if (obj is IEnumerable) {
                EncodeEnumerable((IEnumerable)obj);
            } else if (obj is Enum) {
                EncodeObject(Convert.ChangeType(obj, Enum.GetUnderlyingType(obj.GetType())));
            } else if (obj is JObject) {
                var jobj = (JObject)obj;
                switch (jobj.Kind) {
                case JObjectKind.Array:
                    EncodeEnumerable(jobj.ArrayValue);
                    break;
                case JObjectKind.Boolean:
                    EncodeBool(jobj.BooleanValue);
                    break;
                case JObjectKind.Null:
                    EncodeNull();
                    break;
                case JObjectKind.Number:
                    if (jobj.IsFractional) {
                        EncodeDouble(jobj.DoubleValue);
                    } else if (jobj.IsNegative) {
                        EncodeLong(jobj.LongValue);
                    } else {
                        EncodeULong(jobj.ULongValue);
                    }
                    break;
                case JObjectKind.Object:
                    EncodeDictionary(jobj.ObjectValue);
                    break;
                case JObjectKind.String:
                    EncodeString(jobj.StringValue);
                    break;
                default:
                    throw new ArgumentException("Can't serialize object of type " + obj.GetType().Name, "obj");
                }
            } else {
                throw new ArgumentException("Can't serialize object of type " + obj.GetType().Name, "obj");
            }
        }

        private void EncodeNull() {
            _buffer.Append("null");
        }

        private void EncodeString(string str) {
            _buffer.Append('"');
            foreach (var c in str) {
                if (EscapeChars.ContainsKey(c)) {
                    _buffer.Append(EscapeChars[c]);
                } else {
                    if (c > 0x80 || c < 0x20) {
                        _buffer.Append("\\u" + Convert.ToString(c, 16)
                                                   .ToUpper(CultureInfo.InvariantCulture)
                                                   .PadLeft(4, '0'));
                    } else {
                        _buffer.Append(c);
                    }
                }
            }
            _buffer.Append('"');
        }

        private void EncodeFloat(float f) {
            _buffer.Append(f.ToString(CultureInfo.InvariantCulture));
        }

        private void EncodeDouble(double d) {
            _buffer.Append(d.ToString(CultureInfo.InvariantCulture));
        }

        private void EncodeLong(long l) {
            _buffer.Append(l.ToString(CultureInfo.InvariantCulture));
        }

        private void EncodeULong(ulong l) {
            _buffer.Append(l.ToString(CultureInfo.InvariantCulture));
        }

        private void EncodeBool(bool b) {
            _buffer.Append(b ? "true" : "false");
        }

        private void EncodeDictionary(IDictionary d) {
            var isFirst = true;
            _buffer.Append('{');
            foreach (DictionaryEntry pair in d) {
                if (!(pair.Key is string)) {
                    throw new ArgumentException("Dictionary keys must be strings", "d");
                }
                if (!isFirst) _buffer.Append(',');
                EncodeString((string)pair.Key);
                _buffer.Append(':');
                EncodeObject(pair.Value);
                isFirst = false;
            }
            _buffer.Append('}');
        }

        private void EncodeEnumerable(IEnumerable e) {
            var isFirst = true;
            _buffer.Append('[');
            foreach (var obj in e) {
                if (!isFirst) _buffer.Append(',');
                EncodeObject(obj);
                isFirst = false;
            }
            _buffer.Append(']');
        }
    }
}
