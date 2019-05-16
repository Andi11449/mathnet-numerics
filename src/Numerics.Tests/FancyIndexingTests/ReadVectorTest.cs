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

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Single;
using System;

namespace MathNet.Numerics.Tests.FancyIndexingTests
{
    [TestFixture, Category("FancyIndexing")]
    public class ReadVectorTest
    {

        [Test]
        public void TestSimpleIntegerIndex()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2, 3, 4f, 5f });
            Assert.That(v[3], Is.EqualTo(3f));
            Assert.That(v[1], Is.EqualTo(1f));
        }

        [Test]
        public void TestIntegerEnumerableIndex()
        {
            var v = DenseVector.OfArray(new[] { 0f, 2f, 4f, 6f, 8f, 10f });
            var r = v[new [] { 1, 5, 0 }];
            Assert.That(r, Is.Not.Null);
            Assert.That(r, Is.EqualTo(DenseVector.OfArray(new [] { 2f, 10f, 0f })));
        }

        [Test]
        public void TestIntegerEnumerableIndexOutOfRange()
        {
            var v = DenseVector.OfArray(new[] { 0f, 2f, 4f, 6f, 8f, 10f });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var r = v[new[] { 1, 6, 0 }];
            });
        }

        [Test]
        public void TestIntegerEnumerableIndexEmpty()
        {
            var v = DenseVector.OfArray(new[] { 0f, 2f, 4f, 6f, 8f, 10f });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var r = v[new int[0]];
            });
        }

        [Test]
        public void TestLogicalIndex()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            var r = v[new[] { false, false, true, true, false, true }];
            Assert.That(r, Is.Not.Null);
            Assert.That(r, Is.EqualTo(DenseVector.OfArray(new[] { 2f, 3f, 5f })));
        }

        [Test]
        public void TestLogicalIndexInvalidLength()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var r = v[new[] { false, false, true, true, false }];
            });
        }
    }
}
