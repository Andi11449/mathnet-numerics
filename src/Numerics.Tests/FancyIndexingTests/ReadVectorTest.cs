﻿using NUnit.Framework;
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
            var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var r = v[new[] { false, false, true, true, false }];
            });
        }
    }
}
