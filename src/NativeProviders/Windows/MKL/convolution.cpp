#include "convolution.h"

int VectorConv1D(const float* kernel, const int kernelLength, const float* x, const int xLength, const int firstX, float* result, const int resultLength)
{
    VSLConvTaskPtr task;
    if (vslsConvNewTask1D(&task, VSL_CONV_MODE_AUTO, kernelLength, xLength, resultLength) != VSL_STATUS_OK)
    {
        return ConvolutionStatusTaskCreationFailed;
    }
    vslConvSetStart(task, &firstX);
    auto executionStatus = vslsConvExec1D(task, kernel, 1, x, 1, result, 1);
    if (vslConvDeleteTask(&task) != 0) return ConvolutionStatusTaskDestructionFailed;
    if (executionStatus != VSL_STATUS_OK) return ConvolutionStatusTaskExecutionFailed;
    return ConvolutionStatusSuccess;
}
