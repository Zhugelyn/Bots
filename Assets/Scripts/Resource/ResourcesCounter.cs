using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    [SerializeField] private ResourceReceiver _resourceReceiver;
    
    private Dictionary<ResourceType, int> _resourcesCount;
    public event Action<Dictionary<ResourceType, int>> Changed;

    private void Awake()
    {
        _resourcesCount = new Dictionary<ResourceType, int>();

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            _resourcesCount[type] = 0;
    }

    private void OnEnable()
    {
        _resourceReceiver.ResourceAccepted += ChangeResourceCount;
    }

    private void OnDisable()
    {
        _resourceReceiver.ResourceAccepted -= ChangeResourceCount;
    }

    public int GetResourceCount(ResourceType resourceType)
    {
        if (_resourcesCount.TryGetValue(resourceType, out int count))
            return count;
        
        return 0;
    }

    public bool TryCost(int amount)
    {
        if (_resourcesCount.All(resource => resource.Value >= amount))
        {
            foreach (var type in _resourcesCount.Keys.ToList())
            {
                 _resourcesCount[type] -= amount;
            }
            
            Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
            return true;
        }
        
        return false;
    }

    public void Refund(int amount)
    {
        if (amount <= 0)
            return;

        foreach (var type in _resourcesCount.Keys.ToList())
            _resourcesCount[type] += amount;

        Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
    }

    public void RefundForType(ResourceType type, int amount)
    {
        if (amount <= 0)
            return;

        if (_resourcesCount.TryGetValue(type, out int current) == false)
            _resourcesCount[type] = amount;
        else
            _resourcesCount[type] = current + amount;

        Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
    }
    
    private void ChangeResourceCount(Resource resource)
    {
        ResourceType type = resource.Type;

        if (_resourcesCount.TryGetValue(type, out int current))
        {
            _resourcesCount[type] = current + 3;
            
            Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
        }
    }
}
