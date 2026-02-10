using UnityEngine;

public class Resource : MonoBehaviour, ICollectable
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    private bool _isClaimed;
    
    public ResourceType Type => _type;
    public bool IsClaimed => _isClaimed;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.SetParent(null);

        _isClaimed = false;
        SetColliderEnabled(true);
    }
    
    public void UpdateState(Transform target)
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = false;
        transform.SetParent(target);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public bool TryClaim()
    {
        if (_isClaimed)
            return false;

        _isClaimed = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        SetColliderEnabled(false);
        return true;
    }

    private void SetColliderEnabled(bool enabled)
    {
        _collider.enabled = enabled;
    }
}

public enum ResourceType
{
    Stone,
    Gold,
    Wood
}
