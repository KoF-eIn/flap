using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Transform _container;
    [SerializeField] protected int _initialPoolSize = 5;

    protected ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, _container, _initialPoolSize);
    }

    public virtual T Spawn(Vector3 position, Quaternion rotation)
    {
        return _pool.Get(position, rotation);
    }

    public virtual void Despawn(T obj)
    {
        _pool.Return(obj);
    }

    public virtual void ResetPool()
    {
        _pool.Reset();
    }
}