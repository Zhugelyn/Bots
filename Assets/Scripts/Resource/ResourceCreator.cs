using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class ResourceCreator : UniversalObjectPool<Resource>
{
    [field: SerializeField] public float SpawnDelay { get; private set; }
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.ResourceReceiver.ResourceAccepted += ReturnToPool;
    }

    private void OnDisable()
    {
        _base.ResourceReceiver.ResourceAccepted -= ReturnToPool;
    }

    private void Start()
    {
        StartCoroutine(Create());
    }

    private IEnumerator Create()
    {
        var wait = new WaitForSeconds(SpawnDelay);

        while (enabled)
        {
            var resource = Pool.Get();
            var spawnPoint = GetRandomSpawnPoint();
            resource.Initialize(spawnPoint);

            yield return wait;
        }
    }

    private void ReturnToPool(Resource resource)
    {
        Pool.Release(resource);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        var boxCollider = GetComponent<BoxCollider>();
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;

        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomZ = Random.Range(-size.z / 2f, size.z / 2f);
        float fixedY = 5f;

        return new Vector3(randomX, fixedY, randomZ) + transform.position + center;
    }
}
