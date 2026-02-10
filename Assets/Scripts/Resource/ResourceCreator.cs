using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class ResourceCreator : UniversalObjectPool<Resource>
{
    [SerializeField] private List<ResourceReceiver> _initialReceivers = new List<ResourceReceiver>();

    private BoxCollider _boxCollider;
    private List<ResourceReceiver> _registeredReceivers = new List<ResourceReceiver>();
    
    [field: SerializeField] public float SpawnDelay { get; private set; }
    
    private void OnEnable()
    {
        foreach (var receiver in _initialReceivers)
        {
            if (receiver != null)
                RegisterReceiver(receiver);
        }
    }

    private void OnDisable()
    {
        foreach (var receiver in _registeredReceivers.ToArray())
        {
            UnregisterReceiver(receiver);
        }
    }

    public void RegisterReceiver(ResourceReceiver receiver)
    {
        if (receiver == null)
            return;
        
        if (_registeredReceivers.Contains(receiver))
            return;
        
        receiver.ResourceAccepted -= ReturnToPool;
        receiver.ResourceAccepted += ReturnToPool;
        _registeredReceivers.Add(receiver);
    }

    public void UnregisterReceiver(ResourceReceiver receiver)
    {
        if (receiver == null)
            return;
        
        receiver.ResourceAccepted -= ReturnToPool;
        _registeredReceivers.Remove(receiver);
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