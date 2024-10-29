using Unity.Collections;
using Unity.Jobs;

public struct CalcWithFibonacciJob : IJob
{
    private long divisor;
    public NativeArray<long> fibonacciSerie;

    public CalcWithFibonacciJob(long div, ref NativeArray<long> arr)
    {
        divisor = div;
        fibonacciSerie = arr;
    }
    
    private void FibonacciDivisor()
    {
        for (int i = 0; i < fibonacciSerie.Length; ++i)
        {
            fibonacciSerie[i] /= divisor;
        }
    }
    
    public void Execute()
    {
        FibonacciDivisor();
    }
}
