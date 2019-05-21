#pragma once
#include "mkl_vsl.h"

#define API extern "C" __declspec(dllexport)

#define ConvolutionStatusSuccess 0;
#define ConvolutionStatusTaskCreationFailed 1;
#define ConvolutionStatusTaskExecutionFailed 2;
#define ConvolutionStatusTaskDestructionFailed 3;

API int VectorConv1D(const float* kernel, const int kernelLength, const float* x, const int xLength, const int firstX, float* result, const int resultLength);
