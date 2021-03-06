﻿
// <copyright file="ManagedConvolutionProvider.cs" company="Math.NET">
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
using System.Numerics;
using MathNet.Numerics.Threading;

namespace MathNet.Numerics.Providers.Convolution.ManagedReference
{
    class ManagedConvolutionProvider : IConvolutionProvider
    {

        
        public void Conv1D(float[] kernel, float[] x, int xOffset, float[] y, int yOffset, int length)
        {
            CommonParallel.For(0, length, (a, b) => {
                for (int i = a; i < b; i++)
                {
                    var yi = yOffset + i;
                    if (yi < 0 || yi > y.Length)
                    {
                        throw new ArgumentException("Target index beyond the limits of array: " + yi);
                    }
                    float v = 0;
                    for (int k = 0; k < kernel.Length; k++)
                    {
                        var xi = xOffset + i - k;
                        if (xi < 0 || xi >= x.Length) continue;
                        v += kernel[k] * x[xi];
                    }
                    y[yi] = v;
                }
            });
        }

        
        public void Conv1D(double[] kernel, double[] x, int xOffset, double[] y, int yOffset, int length)
        {
            CommonParallel.For(0, length, (a, b) => {
                for (int i = a; i < b; i++)
                {
                    var yi = yOffset + i;
                    if (yi < 0 || yi > y.Length)
                    {
                        throw new ArgumentException("Target index beyond the limits of array: " + yi);
                    }
                    double v = 0;
                    for (int k = 0; k < kernel.Length; k++)
                    {
                        var xi = xOffset + i - k;
                        if (xi < 0 || xi >= x.Length) continue;
                        v += kernel[k] * x[xi];
                    }
                    y[yi] = v;
                }
            });
        }

        
        public void Conv1D(Complex[] kernel, Complex[] x, int xOffset, Complex[] y, int yOffset, int length)
        {
            CommonParallel.For(0, length, (a, b) => {
                for (int i = a; i < b; i++)
                {
                    var yi = yOffset + i;
                    if (yi < 0 || yi > y.Length)
                    {
                        throw new ArgumentException("Target index beyond the limits of array: " + yi);
                    }
                    Complex v = 0;
                    for (int k = 0; k < kernel.Length; k++)
                    {
                        var xi = xOffset + i - k;
                        if (xi < 0 || xi >= x.Length) continue;
                        v += kernel[k] * x[xi];
                    }
                    y[yi] = v;
                }
            });
        }

        
        public void Conv1D(Complex32[] kernel, Complex32[] x, int xOffset, Complex32[] y, int yOffset, int length)
        {
            CommonParallel.For(0, length, (a, b) => {
                for (int i = a; i < b; i++)
                {
                    var yi = yOffset + i;
                    if (yi < 0 || yi > y.Length)
                    {
                        throw new ArgumentException("Target index beyond the limits of array: " + yi);
                    }
                    Complex32 v = 0;
                    for (int k = 0; k < kernel.Length; k++)
                    {
                        var xi = xOffset + i - k;
                        if (xi < 0 || xi >= x.Length) continue;
                        v += kernel[k] * x[xi];
                    }
                    y[yi] = v;
                }
            });
        }

        
        public void InitializeVerify()
        {
        }

        public bool IsAvailable()
        {
            return true;
        }

        public void FreeResources()
        {
        }

    }
}
