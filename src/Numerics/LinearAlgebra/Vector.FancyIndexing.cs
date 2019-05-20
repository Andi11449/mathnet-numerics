// <copyright file="Vector.FancyIndexing.cs" company="Math.NET">
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

using System.Collections.Generic;
using System.Linq;
using System;

namespace MathNet.Numerics.LinearAlgebra
{
    public abstract partial class Vector<T>
    {

        /// <summary>
        /// Gets/sets a subvector of this vector that contains the elements of this vector in the same order
        /// as given in the provided indices.
        /// </summary>
        /// <param name="indices">
        /// Enumerable of indices into this vector, must contain at least one element.
        /// </param>
        public Vector<T> this[IEnumerable<int> indices]
        {
            get
            {
                var idxArray = indices as int[] ?? indices.ToArray();
                if (idxArray.Length < 1)
                {
                    throw new ArgumentOutOfRangeException("The number of elements in the subvector must be positive");
                }
                var target = Build.SameAs(this, idxArray.Length);
                for (var i = 0; i < idxArray.Length; i++)
                {
                    target[i] = this[idxArray[i]];
                }
                return target;
            }
            set {
                var idxArray = indices as int[] ?? indices.ToArray();
                if (idxArray.Length < 1)
                {
                    throw new ArgumentOutOfRangeException("The number of elements in the subvector must be positive");
                }
                if (value.Count != idxArray.Length && value.Count != 1)
                {
                    throw new ArgumentOutOfRangeException("The value passed to a subarray must either be scalar or have the same number as elements as the indexed subarray");
                }
                if (value.Count == 1)
                {
                    var v = value[0];
                    foreach (int idx in idxArray) this[idx] = v;
                }
                else
                {
                    for (int i = 0; i < idxArray.Length; i++)
                    {
                        this[idxArray[i]] = value[i];
                    }
                }
            }
        }

        /// <summary>
        /// Gets/sets a subvector of this vector containing only the elements where the provided logical
        /// enumerable is set to true.
        /// </summary>
        /// <param name="indices">
        /// Enumerable of booleans indicating which elements of this vector
        /// shall be part of the generated subvector. The number of elements in the indices must match
        /// the number of elements in this vector.
        /// </param>
        public Vector<T> this[IEnumerable<bool> indices]
        {
            get
            {
                var idxArray = indices as bool[] ?? indices.ToArray();
                if (idxArray.Length != this.Count)
                {
                    throw new ArgumentOutOfRangeException("Logical indices must have the same length as the target vector");
                }
                var target = Build.SameAs(this, idxArray.Count(idx => idx == true));
                var j = 0;
                for (var i = 0; i < idxArray.Length; i++)
                {
                    if (idxArray[i])
                    {
                        target[j++] = this[i];
                    }
                }
                return target;
            }
            set
            {
                var idxArray = indices as bool[] ?? indices.ToArray();
                if (idxArray.Length != this.Count)
                {
                    throw new ArgumentOutOfRangeException("Logical indices must have the same length as the target vector");
                }
                if (value.Count != idxArray.Count(idx => idx == true) && value.Count != 1)
                {
                    throw new ArgumentOutOfRangeException("The value passed to a subarray must either be scalar or have the same number as elements as the indexed subarray");
                }
                if (value.Count == 1)
                {
                    var v = value[0];
                    for (int i = 0; i < idxArray.Length; i++)
                    {
                        if (idxArray[i]) this[i] = v;
                    }
                }
                else
                {
                    var j = 0;
                    for (int i = 0; i < idxArray.Length; i++)
                    {
                        if (idxArray[i]) this[i] = value[j++];
                    }
                }
            }
        }

    }
}
