// <copyright file="Indexing.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2019 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MathNet.Numerics.LinearAlgebra
{
    /// <summary>
    /// A convenience class that provides access to the available indexers
    /// </summary>
    public class Indexing
    {

        /// <summary>
        /// An indexer that gets/sets all elements of a vector or all rows/columns of a matrix
        /// </summary>
        public static Indexer All => new AllIndexer();

        /// <summary>
        /// An indexer that gets/sets all elements in a vector or all rows/columns of a matrix in the
        /// order they are provided in the given enumerable. Note that indices might be in arbitrary
        /// order or even duplicated. Note that there is also an implicit conversion from an int[] to
        /// an <see cref="Indexer"/>.
        /// </summary>
        /// <param name="indices">Indices that shall be selected</param>
        /// <returns></returns>
        public static Indexer FromIndices(IEnumerable<int> indices) => new IndexEnumerableIndexer(indices);

        /// <summary>
        /// An indexer that gets/sets all elements in a vector or all rows/columns of a matrix where the
        /// corresponding element of the bool enumerable is true. The number of elements in the enumerable
        /// must match the number of elements in the vector or the number of rows/columns in the matrix.
        /// Note that there is also an implicit conversion from a bool[] to an <see cref="Indexer"/>.
        /// </summary>
        /// <param name="indices">Indices that shall be selected</param>
        /// <returns></returns>
        public static Indexer FromLogical(IEnumerable<bool> indices) => new LogicalEnumerableIndexer(indices);

        /// <summary>
        /// An indexer that gets/sets a series of regularly sampled elements in a vector or rows/columns
        /// in a matrix.
        /// </summary>
        /// <param name="startIncl">First element/row/column that shall be selected (inclusively)</param>
        /// <param name="endExcl">Last element/row/column that shall be selected (exclusively)</param>
        /// <param name="step">Stepsize for the selection, defaults to 1</param>
        /// <returns></returns>
        public static Indexer Range(int startIncl, int endExcl, int step = 1) => new RangeIndexer(startIncl, endExcl, step);

    }

    public abstract class Indexer : IEnumerable<int>
    {

        private int[] _indices;

        public virtual void Init(int vectorLength)
        {
            _indices = CreateIndexArray(vectorLength);
        }

        protected abstract int[] CreateIndexArray(int vectorLength);

        public virtual IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>)_indices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _indices.GetEnumerator();
        }

        public virtual int Count => _indices.Length;

        public virtual int this[int idx]
        {
            get => _indices[idx];
        }

        public static implicit operator Indexer(int[] indices)
        {
            return Indexing.FromIndices(indices);
        }

        public static implicit operator Indexer(bool[] indices)
        {
            return Indexing.FromLogical(indices);
        }

    }

    internal class AllIndexer : Indexer
    {
        protected override int[] CreateIndexArray(int vectorLength)
        {
            var indices = new int[vectorLength];
            for (int i = 0; i < vectorLength; i++)
            {
                indices[i] = i;
            }
            return indices;
        }
    }

    internal class IndexEnumerableIndexer : Indexer
    {
        private readonly IEnumerable<int> _enumerable;
        internal IndexEnumerableIndexer(IEnumerable<int> indices)
        {
            _enumerable = indices;
        }
        protected override int[] CreateIndexArray(int vectorLength)
        {
            var indices = _enumerable as int[] ?? _enumerable.ToArray();
            return indices;
        }
    }

    internal class LogicalEnumerableIndexer : Indexer
    {
        private readonly IEnumerable<bool> _enumerable;
        internal LogicalEnumerableIndexer(IEnumerable<bool> indices)
        {
            _enumerable = indices;
        }
        protected override int[] CreateIndexArray(int vectorLength)
        {
            var logicals = _enumerable as bool[] ?? _enumerable.ToArray();
            if (logicals.Length != vectorLength)
            {
                throw new ArgumentOutOfRangeException("The number of elements in the boolean enumerable must match the number of elements in the indexed vector or columns/rows in a matrix");
            }
            var indices = new int[logicals.Count(logical => logical == true)];
            var j = 0;
            for (int i = 0; i < vectorLength; i++)
            {
                if (logicals[i]) indices[j++] = i;
            }
            return indices;
        }
    }

    internal class RangeIndexer : Indexer
    {
        internal RangeIndexer(int startIncl, int stopExcl, int step = 1)
        {
        }

        protected override int[] CreateIndexArray(int vectorLength)
        {
            throw new System.NotImplementedException();
        }
    }

}
