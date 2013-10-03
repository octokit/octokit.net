// ----------------------------------------------------
// THIS WHOLE File CAN GO AWAY WHEN WE TARGET 4.5 ONLY
// ----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        bool ContainsKey(TKey key);
        bool TryGetValue(TKey key, out TValue value);

        TValue this[TKey key] { get; }
        IEnumerable<TKey> Keys { get; }
        IEnumerable<TValue> Values { get; }
    }

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IReadOnlyList<out TItem> : IReadOnlyCollection<TItem>
    {
        TItem this[int index] { get; }
    }

    public interface IReadOnlyCollection<out T> : IEnumerable<T>
    {
        int Count { get; }
    }

    public class ReadOnlyCollection<TItem> : IReadOnlyList<TItem>
    {
        readonly List<TItem> _source;

        public ReadOnlyCollection(IList<TItem> source)
        {
            _source = new List<TItem>(source);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _source.Count; }
        }

        public TItem this[int index]
        {
            get { return _source[index]; }
        }
    }

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        readonly IDictionary<TKey, TValue> _source;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> source)
        {
            _source = new Dictionary<TKey, TValue>(source);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _source.Count; }
        }

        public bool ContainsKey(TKey key)
        {
            return _source.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _source.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return _source[key]; }
        }

        public IEnumerable<TKey> Keys
        {
            get { return _source.Keys; }
        }

        public IEnumerable<TValue> Values
        {
            get { return _source.Values; }
        }
    }
}