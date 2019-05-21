// <copyright file="TestConvolution1D.cs" company="Math.NET">
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

using System;
using NUnit.Framework;
using MathNet.Numerics.Convolutions;

namespace MathNet.Numerics.Tests.ConvolutionsTests
{

    [TestFixture, Category("Convolution")]
    public class TestConvolution1D
    {

        [Test]
        public void VectorConv1DValidOutArray()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };
            var actual = new float[expected.Length];

            Convolution.Conv1D(h, x, actual);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void VectorConv1DValidReturnArray()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

            var actual = Convolution.Conv1D(h, x);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void VectorConv1DSameOddKernel()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f, -4.4f };

            var actual = Convolution.Conv1D(h, x, Padding.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void VectorConv1DSameEvenKernel()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f, 0.8f };
            var expected = new float[] { -1.9f, 23.5f, -11.1f, -8.2f, 16.2f, 7.3f, -0.2f, -1.4f, -5.2f, 3.1f };
            var actual = new float[expected.Length];

            Convolution.Conv1D(h, x, actual, Padding.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void VectorConv1DCausal()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { 6f, -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

            var actual = Convolution.Conv1D(h, x, Padding.Causal);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

    }
}
