using System.Collections.Generic;
using System.Linq;

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


    }
}
