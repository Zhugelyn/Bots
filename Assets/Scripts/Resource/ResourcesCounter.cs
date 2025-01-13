using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourcesCounter : MonoBehaviour
{
    [SerializeField] private Base _base; 
    
    public event Action<Dictionary<ResourceType, int>> Changed;
    
    private Dictionary<ResourceType, int> _resourcesCount;

    private void Awake()
    {
        _resourcesCount = new Dictionary<ResourceType, int>();
    }

    private void OnEnable()
    {
        _base.ResourceReceiver.ResourceAccepted += ChangeResourceCount;
    }

    private void OnDisable()
    {
        _base.ResourceReceiver.ResourceAccepted -= ChangeResourceCount;
    }
    
    private void ChangeResourceCount(Resource resource)
    {
        int startResourceCount = 1;
        ResourceType type = resource.GetType();
        var needType = _resourcesCount.Where(r => r.Key == type);

        if (needType.Any())
            _resourcesCount[type]++;
        else
            _resourcesCount.Add(resource.GetType(), startResourceCount);
        
        Changed?.Invoke(_resourcesCount);
    }
}
