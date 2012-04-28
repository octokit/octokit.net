using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Burr.SimpleJSON {
    public class ParseError : Exception {
        public readonly int Position;

        public ParseError(string message, int position) : base(message) {
            Position = position;
        }
    }

    public static class JSONDecoder {
        private const char ObjectStart = '{';
        private const char ObjectEnd = '}';
        private const char ObjectPairSeparator = ',';
        private const char ObjectSeparator = ':';
        private const char ArrayStart = '[';
        private const char ArrayEnd = ']';
        private const char ArraySeparator = ',';
        private const char StringStart = '"';
        private const char NullStart = 'n';
        private const char TrueStart = 't';
        private const char FalseStart = 'f';

        private static readonly Regex NumberRegex = new Regex("(-?(?:0|[1-9]\\d*))(\\.\\d+)?([eE][-+]?\\d+)?",
                                                              RegexOptions.CultureInvariant | RegexOptions.Multiline);

        public static JObject Decode(string json) {
            var data = Scan(json, 0);
            return data.Result;
        }

        private struct ScannerData {
            public readonly JObject Result;
            public readonly int Index;

            public ScannerData(JObject result, int index) {
                Result = result;
                Index = index;
            }
        }

        private static readonly Dictionary<char, string> EscapeChars =
            new Dictionary<char, string>
                {
                    { '"', "\"" },
                    { '\\', "\\" },
                    { 'b', "\b" },
                    { 'f', "\f" },
                    { 'n', "\n" },
                    { 'r', "\r" },
                    { 't', "\t" },
                };

        private static ScannerData Scan(string json, int index) {
            index = SkipWhitespace(json, index);
            var nextChar = json[index];

            switch (nextChar) {
            case StringStart:
                return ScanString(json, index + 1);
            case TrueStart:
                return ScanTrue(json, index);
            case FalseStart:
                return ScanFalse(json, index);
            case NullStart:
                return ScanNull(json, index);
            case ArrayStart:
                return ScanArray(json, index);
            case ObjectStart:
                return ScanObject(json, index);
            default:
                if (IsNumberStart(nextChar)) {
                    return ScanNumber(json, index);
                }
                throw new ParseError("Unexpected token " + nextChar, index);
            }
        }

        private static ScannerData ScanString(string json, int index) {
            var strBuilder = new StringBuilder();
            while (json[index] != '"') {
                if (json[index] == '\\') {
                    ++index;
                    if (EscapeChars.ContainsKey(json[index])) {
                        strBuilder.Append(EscapeChars[json[index]]);
                        ++index;
                    } else if (json[index] == 'u') {
                        ++index;
                        var unicodeSequence = Convert.ToInt32(json.Substring(index, 4), 16);
                        strBuilder.Append((char)unicodeSequence);
                        index += 4;
                    }
                } else {
                    strBuilder.Append(json[index]);
                    ++index;
                }
            }
            return new ScannerData(JObject.CreateString(strBuilder.ToString()), index + 1);
        }

        private static ScannerData ScanTrue(string json, int index) {
            return new ScannerData(JObject.CreateBoolean(true), ExpectConstant(json, index, "true"));
        }

        private static ScannerData ScanFalse(string json, int index) {
            return new ScannerData(JObject.CreateBoolean(false), ExpectConstant(json, index, "false"));
        }

        private static ScannerData ScanNull(string json, int index) {
            return new ScannerData(JObject.CreateNull(), ExpectConstant(json, index, "null"));
        }

        private static ScannerData ScanNumber(string json, int index) {
            var match = NumberRegex.Match(json, index);

            if (!match.Success) {
                throw new ParseError("Could not parse number", index);
            }

            return new ScannerData(JObject.CreateNumber(match.Groups[1].Value,
                                                        match.Groups[2].Value,
                                                        match.Groups[3].Value), index + match.Groups[0].Length);
        }

        private static ScannerData ScanArray(string json, int index) {
            var list = new List<JObject>();

            var nextTokenIndex = SkipWhitespace(json, index + 1);
            if (json[nextTokenIndex] == ArrayEnd) return new ScannerData(JObject.CreateArray(list), nextTokenIndex + 1);

            while (json[index] != ArrayEnd) {
                ++index;
                var result = Scan(json, index);
                index = SkipWhitespace(json, result.Index);
                if (json[index] != ArraySeparator && json[index] != ArrayEnd) {
                    throw new ParseError("Expecting array separator (,) or array end (])", index);
                }
                list.Add(result.Result);
            }
            return new ScannerData(JObject.CreateArray(list), index + 1);
        }

        private static ScannerData ScanObject(string json, int index) {
            var dict = new Dictionary<string, JObject>();

            var nextTokenIndex = SkipWhitespace(json, index + 1);
            if (json[nextTokenIndex] == ObjectEnd) return new ScannerData(JObject.CreateObject(dict), nextTokenIndex + 1);

            while (json[index] != ObjectEnd) {
                ++index;
                var keyResult = Scan(json, index);
                if (keyResult.Result.Kind != JObjectKind.String) {
                    throw new ParseError("Object keys must be strings", index);
                }
                index = SkipWhitespace(json, keyResult.Index);
                if (json[index] != ObjectSeparator) {
                    throw new ParseError("Expecting object separator (:)", index);
                }
                ++index;
                var valueResult = Scan(json, index);
                index = SkipWhitespace(json, valueResult.Index);
                if (json[index] != ObjectEnd && json[index] != ObjectPairSeparator) {
                    throw new ParseError("Expecting object pair separator (,) or object end (})", index);
                }
                dict[keyResult.Result.StringValue] = valueResult.Result;
            }
            return new ScannerData(JObject.CreateObject(dict), index + 1);
        }

        private static int SkipWhitespace(string json, int index) {
            while (char.IsWhiteSpace(json[index])) {
                ++index;
            }
            return index;
        }

        private static int ExpectConstant(string json, int index, string expected) {
            if (json.Substring(index, expected.Length) != expected) {
                throw new ParseError(string.Format("Expected '{0}' got '{1}'",
                                                   expected,
                                                   json.Substring(index, expected.Length)),
                                     index);
            }
            return index + expected.Length;
        }

        private static bool IsNumberStart(char b) {
            return b == '-' || (b >= '0' && b <= '9');
        }
    }
}