using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class ResourceCreator : UniversalObjectPool<Resource>
{
    [SerializeField] private ResourceReceiver _resourceReceiver;
    [SerializeField] private LayerMask _groundMask = -1;
    [SerializeField, Min(0f)] private float _raycastExtraHeight = 25f;
    [SerializeField, Min(0.1f)] private float _raycastMaxDistance = 250f;
    
    private BoxCollider _boxCollider;
    
    [field: SerializeField] public float SpawnDelay { get; private set; }
    
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
        _boxCollider = GetComponent<BoxCollider>();
        
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
        Bounds bounds = _boxCollider.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        Vector3 rayOrigin = new Vector3(randomX, bounds.max.y + _raycastExtraHeight, randomZ);

        if (Physics.Raycast(
                rayOrigin,
                Vector3.down,
                out RaycastHit hit,
                _raycastMaxDistance,
                _groundMask,
                QueryTriggerInteraction.Ignore))
        {
            return hit.point;
        }

        // Fallback: at least keep it inside spawner bounds.
        return new Vector3(randomX, bounds.center.y, randomZ);
    }
}