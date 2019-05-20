using System;
using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra;

namespace MathNet.Numerics.Tests.FancyIndexingTests
{
    [TestFixture, Category("FancyIndexing")]
    public class ReadMatrixTest
    {
        [Test]
        public void GetSimpleIntegerIndex()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            Assert.That(m[2, 1], Is.EqualTo(32));
            Assert.That(m[0, 2], Is.EqualTo(13));
            Assert.That(m[3, 0], Is.EqualTo(41));
        }

        [Test]
        public void GetRowVector()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            var row = m[2, Indexing.All];
            Assert.That(row, Is.Not.Null);
            Assert.That(row, Is.EqualTo(DenseVector.OfArray(new float[] { 31, 32, 33 })));
        }

        [Test]
        public void GetRowSubVector()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            var row = m[1, new[] {0, 2}];
            Assert.That(row, Is.Not.Null);
            Assert.That(row, Is.EqualTo(DenseVector.OfArray(new float[] { 21, 23 })));
        }

        [Test]
        public void GetColumnSubVector()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            var col = m[new[] { true, false, false, true}, 2];
            Assert.That(col, Is.Not.Null);
            Assert.That(col, Is.EqualTo(DenseVector.OfArray(new float[] { 13, 43 })));
        }

    }
}
