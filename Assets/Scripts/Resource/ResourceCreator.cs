using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class ResourceCreator : UniversalObjectPool<Resource>
{
    [SerializeField] private ResourceProvider _resourceProvider;

    private BoxCollider _boxCollider;

    [field: SerializeField] public float SpawnDelay { get; private set; }

    private void OnEnable()
    {
        _resourceProvider.ResourceRemoved += OnResourceRemoved;
    }

    private void OnDisable()
    {
        _resourceProvider.ResourceRemoved -= OnResourceRemoved;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        var wait = new WaitForSeconds(SpawnDelay);

        while (enabled)
        {
            Resource resource = Pool.Get();
            resource.Initialize(GetRandomSpawnPoint());
            yield return wait;
        }
    }

    private void OnResourceRemoved(Resource resource)
    {
        if (resource.Type == Prefab.Type)
            Pool.Release(resource);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 center = _boxCollider.center;
        Vector3 size = _boxCollider.size;

        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomZ = Random.Range(-size.z / 2f, size.z / 2f);
        float fixedY = 3f;

        return new Vector3(randomX, fixedY, randomZ) + transform.position + center;
    }
}
