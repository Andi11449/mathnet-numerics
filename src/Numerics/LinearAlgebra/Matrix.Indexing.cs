using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathNet.Numerics.LinearAlgebra
{
    public abstract partial class Matrix<T>
    {

        public Vector<T> this[int row, Indexer columns]
        {
            get {
                columns.Init(ColumnCount);
                var target = Vector<T>.Build.Dense(columns.Count);
                for (var i = 0; i < columns.Count; i++)
                {
                    target[i] = this[row, columns[i]];
                }
                return target;
            }
        }

        public Vector<T> this[Indexer rows, int column]
        {
            get
            {
                rows.Init(RowCount);
                var target = Vector<T>.Build.Dense(rows.Count);
                for (var i = 0; i < rows.Count; i++)
                {
                    target[i] = this[rows[i], column];
                }
                return target;
            }
        }

    }
}
