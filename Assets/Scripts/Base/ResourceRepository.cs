using System.Collections.Generic;
using UnityEngine;

public class ResourceRepository : MonoBehaviour
{
    [SerializeField] private ResourceReceiver _receiver;

    private List<Resource> _resources;
    
    private void OnDisable()
    {
        _receiver.ResourceAccepted -= Add;
    }

    private void Awake()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        _resources = new List<Resource>();

        _receiver.ResourceAccepted += Add;
    }

    private void Add(Resource resource) => 
        _resources.Add(resource);
}
