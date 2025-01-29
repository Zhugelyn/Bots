using System.Collections;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class ResourceCreator : UniversalObjectPool<Resource>
{
    [field: SerializeField] public float SpawnDelay { get; private set; }
    [SerializeField] private ResourceReceiver _resourceReceiver;

    private void OnEnable()
    {
        _resourceReceiver.ResourceAccepted += ReturnToPool;
    }

    private void OnDisable()
    {
        _resourceReceiver.ResourceAccepted -= ReturnToPool;
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
            Resource resource = Pool.Get();
            Vector3 spawnPoint = GetRandomSpawnPoint();
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
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;

        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomZ = Random.Range(-size.z / 2f, size.z / 2f);
        float fixedY = 3f;

        return new Vector3(randomX, fixedY, randomZ) + transform.position + center;
    }
}