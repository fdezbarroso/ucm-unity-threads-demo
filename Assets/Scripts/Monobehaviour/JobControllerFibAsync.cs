using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobControllerFibAsync : MonoBehaviour
{
    private NativeArray<long> result;
    private JobHandle handle;
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
            if (handle.IsCompleted)
            {
                handle.Complete();
                for (int i = 0; i < num; ++i)
                {
                    Debug.Log(result[i]);
                }
                result.Dispose();
                init = false;
            }
        }
    }
    
    protected void LaunchJob()
    {
        init = true;
        result = new NativeArray<long>(num, Allocator.TempJob);
        FibonacciJob fibJob = new FibonacciJob(num, ref result);
        handle = fibJob.Schedule();
    }
}
