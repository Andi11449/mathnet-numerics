﻿// <copyright file="ConvolutionControl.cs" company="Math.NET">
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

namespace MathNet.Numerics.Providers.Convolution
{
    public static class ConvolutionControl
    {

        const string EnvVarConvolutionProvider = "MathNetNumericsConvolutionProvider";
        const string EnvVarConvolutionProviderPath = "MathNetNumericsConvolutionProviderPath";

        private static IConvolutionProvider _provider = new ManagedReference.ManagedConvolutionProvider();

        public static IConvolutionProvider Provider
        {
            get {
                return _provider;
            }
        }

        /// <summary>
        /// Optional path to try to load native provider binaries from.
        /// If not set, Numerics will fall back to the environment variable
        /// `MathNetNumericsFFTProviderPath` or the default probing paths.
        /// </summary>
        public static string HintPath { get; set; }

        internal static void UseManaged()
        {
            _provider = new ManagedReference.ManagedConvolutionProvider();
        }

        /// <summary>
        /// Use a specific provider if configured, e.g. using the
        /// "MathNetNumericsFFTProvider" environment variable,
        /// or fall back to the best provider.
        /// </summary>
        public static void UseDefault()
        {
#if NATIVE
            var value = Environment.GetEnvironmentVariable(EnvVarConvolutionProvider);
            switch (value != null ? value.ToUpperInvariant() : string.Empty)
            {

                case "MKL":
                    UseNativeMKL();
                    break;

                default:
                    UseBest();
                    break;
            }
#else
            UseBest();
#endif
        }

        /// <summary>
        /// Use the best provider available.
        /// </summary>
        public static void UseBest()
        {
#if NATIVE
            if (!TryUseNativeMKL())
            {
                UseManaged();
            }
#else
            UseManaged();
#endif
        }

#if NATIVE
        internal static void UseNativeMKL()
        {
            _provider = new Mkl.MklConvolutionProvider(HintPath);
        }



        internal static bool TryUseNativeMKL()
        {
            return TryUse(new Mkl.MklConvolutionProvider(HintPath));
        }
#endif

        internal static void FreeResources()
        {
            Provider.FreeResources();
        }

        static bool TryUse(IConvolutionProvider provider)
        {
            try
            {
                if (!provider.IsAvailable())
                {
                    return false;
                }

                _provider = provider;
                return true;
            }
            catch
            {
                // intentionally swallow exceptions here - use the explicit variants if you're interested in why
                return false;
            }
        }
    }
}
