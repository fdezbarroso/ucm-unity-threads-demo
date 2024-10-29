using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
public class JobControllerPlanning : MonoBehaviour
{
    private NativeArray<long> result;
    private JobHandle handle;
    private JobHandle secondHandle;
    private bool init = false;
    private int num = 1000;
    
    void Update()
    {
        if (!init)
        {
            if (Input.anyKey)
            {
                LaunchJob();
            }
        }
        else
        {
            if (secondHandle.IsCompleted)
            {
                secondHandle.Complete();
                init = false;
                for (int i = 0; i < num; ++i)
                {
                    Debug.Log(result[i]);
                }
                result.Dispose();
            }
        }
    }
    
    protected void LaunchJob()
    {
        init = true;
        result = new NativeArray<long>(num, Allocator.Persistent);
        FibonacciJob fibJob = new FibonacciJob(num, ref result);
        CalcWithFibonacciJob calcWitJob = new CalcWithFibonacciJob(num, ref
        result);
        handle = fibJob.Schedule();
        secondHandle = calcWitJob.Schedule(handle);
    }
}
