// <copyright file="Convolution.cs" company="Math.NET">
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
using MathNet.Numerics.Providers.Convolution;
using Complex = System.Numerics.Complex;

namespace MathNet.Numerics.Convolutions
{

    public partial class Convolution
    {


        public static void Conv1D(float[] kernel, float[] x, float[] y, Padding padding = Padding.Valid)
        {
            ConvolutionControl.Provider.Conv1D(kernel, x, GetXOffset(kernel.Length, padding), y, 0, y.Length);
        }

        public static float[] Conv1D(float[] kernel, float[] x, Padding padding = Padding.Valid)
        {
            var y = new float[GetExpectedResultLength(kernel.Length, x.Length, padding)];
            Conv1D(kernel, x, y, padding);
            return y;
        }


        public static void Conv1D(double[] kernel, double[] x, double[] y, Padding padding = Padding.Valid)
        {
            ConvolutionControl.Provider.Conv1D(kernel, x, GetXOffset(kernel.Length, padding), y, 0, y.Length);
        }

        public static double[] Conv1D(double[] kernel, double[] x, Padding padding = Padding.Valid)
        {
            var y = new double[GetExpectedResultLength(kernel.Length, x.Length, padding)];
            Conv1D(kernel, x, y, padding);
            return y;
        }


        public static void Conv1D(Complex32[] kernel, Complex32[] x, Complex32[] y, Padding padding = Padding.Valid)
        {
            ConvolutionControl.Provider.Conv1D(kernel, x, GetXOffset(kernel.Length, padding), y, 0, y.Length);
        }

        public static Complex32[] Conv1D(Complex32[] kernel, Complex32[] x, Padding padding = Padding.Valid)
        {
            var y = new Complex32[GetExpectedResultLength(kernel.Length, x.Length, padding)];
            Conv1D(kernel, x, y, padding);
            return y;
        }


        public static void Conv1D(Complex[] kernel, Complex[] x, Complex[] y, Padding padding = Padding.Valid)
        {
            ConvolutionControl.Provider.Conv1D(kernel, x, GetXOffset(kernel.Length, padding), y, 0, y.Length);
        }

        public static Complex[] Conv1D(Complex[] kernel, Complex[] x, Padding padding = Padding.Valid)
        {
            var y = new Complex[GetExpectedResultLength(kernel.Length, x.Length, padding)];
            Conv1D(kernel, x, y, padding);
            return y;
        }


        private static int GetXOffset(int kernelLength, Padding padding)
        {
            switch (padding)
            {
                case Padding.Valid:
                    return kernelLength - 1;
                case Padding.Same:
                    return kernelLength / 2;
                case Padding.Causal:
                    return 0;
                default:
                    throw new InvalidOperationException("Unsupported padding type: " + padding);
            }
        }

        private static int GetExpectedResultLength(int kernelLength, int xLength, Padding padding)
        {
            switch (padding)
            {
                case Padding.Valid:
                    return xLength - kernelLength + 1;
                case Padding.Same:
                case Padding.Causal:
                    return xLength;
                default:
                    throw new InvalidOperationException("Unsupported padding type: " + padding);
            }
        }

    }

}
