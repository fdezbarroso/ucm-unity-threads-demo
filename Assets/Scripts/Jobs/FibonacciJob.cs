using Unity.Collections;
using Unity.Jobs;

public struct FibonacciJob : IJob
{
    private int n;
    public NativeArray<long> fibonacciSerie;
    public FibonacciJob(int a_n, ref NativeArray<long> arr)
    {
        n = a_n;
        fibonacciSerie = arr;
    }
    private void Fibonacci()
    {
        long aux, a, b;
        b = 1;
        a = 0;
        for (int i = 0; i < n; ++i)
        {
            aux = a;
            a = b;
            b = aux + a;
            fibonacciSerie[i] = aux;
        }
    }
    public void Execute()
    {
        Fibonacci();
    }
}
