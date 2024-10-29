using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
public class GeneratorImproved : MonoBehaviour
{
    public int numInstances;
    public GameObject prefab;
    public Transform target;
    public float speed;
    public float rangeMin;
    public float rangeMax;
    public bool paralell;
    Transform[] transforms;
    TransformAccessArray transAccArr;
    JobHandle handle;
    void Start()
    {
        transforms = new Transform[numInstances];
        for (int i = 0; i < numInstances; ++i)
        {
            Vector3 position = new Vector3(Random.Range(rangeMin, rangeMax), 0f,
            Random.Range(rangeMin, rangeMax));
            GameObject go = Instantiate(prefab, position, Quaternion.identity);
            transforms[i] = go.transform;
        }
        transAccArr = new TransformAccessArray(transforms);
    }
    private void OnDisable()
    {
        transAccArr.Dispose();
    }
    void Update()
    {
        //MovingTransformsParallel
        if (paralell)
            RunParallel();
        else
            RunSecuential();
    }
    private void RunParallel()
    {
        handle.Complete();
        MovingTransformsParallel job = new
        MovingTransformsParallel(target.position, 1f, speed, Time.deltaTime);
        handle = job.Schedule(transAccArr);
        JobHandle.ScheduleBatchedJobs();
    }
    private void RunSecuential()
    {
        for (int i = 0; i < transforms.Length; ++i)
        {
            Vector3 direction = target.position - transforms[i].position;
            if (direction.sqrMagnitude > 1f)
            {
                transforms[i].position += direction.normalized * speed *
                Time.deltaTime;
            }
        }
    }
}
