using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FromCenter
{
    public static class IterationExtensions
    {
        /// <summary>
        /// Iterates a <see cref="IEnumerable{T}"/> from the center
        /// alternating between the right and left sides.
        /// </summary>
        /// <param name="enumerable">
        /// The collection of items to iterate over.
        /// </param>
        /// <param name="startWithRightSide">
        /// If the first side to visit after the center should be the right side,
        /// otherwise it will be the left side.
        /// </param>
        public static IEnumerable<T> FromCenter<T>(this IEnumerable<T> enumerable, bool startWithRightSide = false) =>
            new FromCenterEnumerable<T>(enumerable, startWithRightSide);
    }

    public sealed class FromCenterEnumerable<T> : IEnumerable<T>, IDisposable
    {
        readonly FromCenterEnumerator<T> enumerator;

        public FromCenterEnumerable(IEnumerable<T> enumerable, bool startWithRightSide = false) =>
            enumerator = new FromCenterEnumerator<T>(enumerable, startWithRightSide);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => enumerator;

        public void Dispose()
        {
            enumerator.Dispose();
            GC.SuppressFinalize(this);
        }

        public static int GetCenter(int length, bool startWithRightSide) =>
            (int)Math.Ceiling(length / 2f) - ((length % 2f != 0 || startWithRightSide) ? 1 : 0);

        public static int GetCurrentIndex(int index, int center, bool startWithRightSide) =>
            (int)(Math.Pow(-1, index - (startWithRightSide ? 1 : 0)) * Math.Ceiling(index / 2f)) + center;
    }

    public sealed class FromCenterEnumerator<T> : IEnumerator<T>
    {
        private int i = -1;
        private readonly int length;
        private readonly int center;
        private readonly bool startWithRightSide;
        private readonly IEnumerable<T> enumerable;

        public FromCenterEnumerator(IEnumerable<T> enumerable, bool startWithRightSide = false)
        {
            this.enumerable = enumerable;
            this.startWithRightSide = startWithRightSide;
            length = enumerable.Count();
            center = GetCenter(length, startWithRightSide);
        }

        public T Current => enumerable.ElementAt(GetCurrentIndex(i, center, startWithRightSide));

        object? IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (++i >= length) return false;
            return true;
        }

        public void Reset() => i = -1;

        private static int GetCenter(int length, bool startWithRightSide) =>
            (int)Math.Ceiling(length / 2f) - ((length % 2f != 0 || startWithRightSide) ? 1 : 0);

        private static int GetCurrentIndex(int index, int center, bool startWithRightSide) =>
            (int)(Math.Pow(-1, index - (startWithRightSide ? 1 : 0)) * Math.Ceiling(index / 2f)) + center;
    }
}
