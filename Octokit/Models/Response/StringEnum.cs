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
        private readonly string _stringValue;

        private TEnum? _parsedValue;

        public StringEnum(TEnum parsedValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), parsedValue))
            {
                throw GetArgumentException(parsedValue.ToString());
            }

            // Use the SimpleJsonSerializer to serialize the TEnum into the correct string according to the GitHub Api strategy
            _stringValue = SimpleJsonSerializer.SerializeEnum(parsedValue as Enum);
            _parsedValue = parsedValue;
        }

        public StringEnum(string stringValue)
        {
            Ensure.ArgumentNotNullOrEmptyString(stringValue, nameof(stringValue));

            _stringValue = stringValue;
            _parsedValue = null;
        }

        public string StringValue
        {
            get { return _stringValue; }
        }

        public TEnum Value
        {
            get { return _parsedValue ?? (_parsedValue = ParseValue()).Value; }
        }

        internal string DebuggerDisplay
        {
            get
            {
                TEnum value;
                if (TryParse(out value))
                {
                    return value.ToString();
                }

                return StringValue;
            }
        }

        public bool TryParse(out TEnum value)
        {
            if (_parsedValue.HasValue)
            {
                // the value has been parsed already.
                value = _parsedValue.Value;
                return true;
            }

            try
            {
                // Use the SimpleJsonSerializer to parse the string to Enum according to the GitHub Api strategy
                value = (TEnum)SimpleJsonSerializer.DeserializeEnum(StringValue, typeof(TEnum));

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
            TEnum value;
            TEnum otherValue;
            if (TryParse(out value) && other.TryParse(out otherValue))
            {
                // if we're able to parse both values, compare the parsed enum
                return value.Equals(otherValue);
            }

            // otherwise, we fall back to a case-insensitive comparison of the string values.
            return string.Equals(StringValue, other.StringValue, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is StringEnum<TEnum> && Equals((StringEnum<TEnum>)obj);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(StringValue);
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
            return StringValue;
        }

        private TEnum ParseValue()
        {
            TEnum value;
            if (TryParse(out value))
            {
                return value;
            }

            throw GetArgumentException(StringValue);
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