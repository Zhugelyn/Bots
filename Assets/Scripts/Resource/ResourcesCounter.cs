using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    [SerializeField] private Base @base; 
    
    public event Action<Dictionary<ResourceType, int>> Changed;
    
    private Dictionary<ResourceType, int> _resourcesCount;

    private void Awake()
    {
        _resourcesCount = new Dictionary<ResourceType, int>();
    }

    private void OnEnable()
    {
        @base.ResourceReceiver.ResourceAccepted += ChangeResourceCount;
    }

    private void OnDisable()
    {
        @base.ResourceReceiver.ResourceAccepted -= ChangeResourceCount;
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
