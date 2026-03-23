using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    public ResourceType Type => _type;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.SetParent(null);
        _collider.enabled = true;
    }

    public void AttachTo(Transform target)
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
    }
}

public enum ResourceType
{
    Stone,
    Gold,
    Wood
}
