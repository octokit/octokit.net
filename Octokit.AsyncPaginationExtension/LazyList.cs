using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace Octokit.AsyncPaginationExtension
{
    internal class LazyList<T> : IReadOnlyList<T>
    {
        private readonly Func<int, T> _generator;
        private readonly List<T?> _list = new();

        public LazyList(Func<int, T> generator)
        {
            _generator = generator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var i = 0;
            while (true) yield return this[i++];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => int.MaxValue;

        public T this[int index]
        {
            get
            {
                if (_list.Count <= index) _list.AddRange(Enumerable.Repeat<T?>(default, index - _list.Count + 1));
                return _list[index] ??= _generator(index);
            }
        }
    }
}
