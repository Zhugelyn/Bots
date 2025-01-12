using UnityEngine;
using UnityEngine.Pool;

public abstract class UniversalObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: OnCreate,
        actionOnGet: (obj) => OnGet(obj),
        actionOnRelease: (obj) => OnRelease(obj),
        maxSize: _poolMaxSize);
    }

    protected virtual T OnCreate()
    {
        return Instantiate(Prefab);
    }

    protected virtual void OnGet(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}