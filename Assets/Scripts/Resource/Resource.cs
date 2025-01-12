using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Resource : MonoBehaviour, ICollectable
{
    [SerializeField] private ResourceType _type;
    
    public bool IsScaned;

    public void Initialize(Vector3 position)
    {
        IsScaned = false;
        transform.position = position;
        gameObject.SetActive(true);
    }

    new public ResourceType GetType()
    {
        return _type;
    }
}

public enum ResourceType
{
    Stone,
    Gold,
    Log
}
