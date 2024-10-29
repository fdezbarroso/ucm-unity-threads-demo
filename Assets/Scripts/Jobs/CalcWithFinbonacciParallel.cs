using Unity.Collections;
using Unity.Jobs;

public struct CalcWithFinbonacciParallel : IJobParallelFor
{
    private long divisor;
    public NativeArray<long> fibonacciSerie;

    public CalcWithFinbonacciParallel(long div, ref NativeArray<long> arr)
    {
        divisor = div;
        fibonacciSerie = arr;
    }
    
    public void Execute(int index)
    {
        fibonacciSerie[index] /= divisor;
    }
}
