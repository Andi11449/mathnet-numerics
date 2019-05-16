using System.Collections.Generic;
using System.Linq;
using System;

namespace MathNet.Numerics.LinearAlgebra
{
    public abstract partial class Vector<T>
    {

        public Vector<T> this[IEnumerable<int> indices]
        {
            get
            {
                var idxArray = indices as int[] ?? indices.ToArray();
                var target = Build.SameAs(this, idxArray.Length);
                for (var i = 0; i < idxArray.Length; i++)
                {
                    target[i] = this[idxArray[i]];
                }
                return target;
            }
        }

        public Vector<T> this[IEnumerable<bool> indices]
        {
            get
            {
                var idxArray = indices as bool[] ?? indices.ToArray();
                if (idxArray.Length != this.Count)
                {
                    throw new IndexOutOfRangeException("Logical indices must have the same length as the target vector");
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
        }


    }
}
