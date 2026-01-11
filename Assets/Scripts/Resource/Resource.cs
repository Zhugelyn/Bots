using UnityEngine;

[RequireComponent(typeof(Collider)),
RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, ICollectable
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private Rigidbody _rigidbody;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _rigidbody.useGravity = true;
        transform.SetParent(null);
    }
    
    public void UpdateState(Transform target)
    {
        _rigidbody.useGravity = false;
        transform.SetParent(target);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public ResourceType GetResourceType()
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
