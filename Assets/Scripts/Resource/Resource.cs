using UnityEngine;

[RequireComponent(typeof(Collider)),
RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, ICollectable
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private Rigidbody _rigidbody;

    private Worker _worker;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _rigidbody.useGravity = true;
        transform.SetParent(null);
    }

    public void Subscribe(Worker worker)
    {
        if (_worker != null)
            return;

        _worker = worker;
        _worker.Animation.PickUpFinished += UpdateState;
    }
        
    public void Unsubscribe()
    {
        if (_worker == null)
            return;
        
        _worker.Animation.PickUpFinished -= UpdateState;
    }
    
    private void UpdateState()
    {
        _rigidbody.useGravity = false;
        transform.SetParent(_worker.ResourcePosition);
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
