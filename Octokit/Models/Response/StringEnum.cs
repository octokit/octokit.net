using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public struct StringEnum<TEnum> : IEquatable<StringEnum<TEnum>>
        where TEnum : struct
    {
        private readonly string _value;

        private TEnum? _parsedValue;

        public StringEnum(TEnum parsedValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), parsedValue))
            {
                throw GetArgumentException(parsedValue.ToString());
            }

            _value = parsedValue.ToString();
            _parsedValue = parsedValue;
        }

        public StringEnum(string value)
        {
            _value = value;
            _parsedValue = null;
        }

        public string Value
        {
            get { return _value; }
        }

        public TEnum ParsedValue
        {
            get { return _parsedValue ?? (_parsedValue = ParseValue()).Value; }
        }

        internal string DebuggerDisplay
        {
            get { return Value; }
        }

        public bool TryParse(out TEnum value)
        {
            if (_parsedValue.HasValue)
            {
                // the value has been parsed already.
                value = _parsedValue.Value;
                return true;
            }

            if (string.IsNullOrEmpty(Value))
            {
                value = default(TEnum);
                return false;
            }

            try
            {
                // Use the SimpleJsonSerializer to parse the string to Enum according to the GitHub Api strategy
                value = (TEnum)new SimpleJsonSerializer().DeserializeEnum(Value, typeof(TEnum));

                // cache the parsed value for subsequent calls.
                _parsedValue = value;
                return true;
            }
            catch (ArgumentException)
            {
                value = default(TEnum);
                return false;
            }
        }

        public bool Equals(StringEnum<TEnum> other)
        {
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is StringEnum<TEnum> && Equals((StringEnum<TEnum>) obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0;
        }

        public static bool operator ==(StringEnum<TEnum> left, StringEnum<TEnum> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StringEnum<TEnum> left, StringEnum<TEnum> right)
        {
            return !left.Equals(right);
        }

        public static implicit operator StringEnum<TEnum>(string value)
        {
            return new StringEnum<TEnum>(value);
        }

        public static implicit operator StringEnum<TEnum>(TEnum parsedValue)
        {
            return new StringEnum<TEnum>(parsedValue);
        }

        public override string ToString()
        {
            return Value ?? "null";
        }

        private TEnum ParseValue()
        {
            TEnum value;
            if (TryParse(out value))
            {
                return value;
            }

            throw GetArgumentException(ToString());
        }

        private static ArgumentException GetArgumentException(string value)
        {
            return new ArgumentException(string.Format(
                CultureInfo.InvariantCulture,
                "Value '{0}' is not a valid '{1}' enum value.",
                value,
                typeof(TEnum).Name));
        }
    }
}