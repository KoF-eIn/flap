using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool = new Queue<T>();

    public ObjectPool(T prefab, Transform parent, int initialSize = 0)
    {
        _prefab = prefab;
        _parent = parent;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNew();
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    private T CreateNew()
    {
        return Object.Instantiate(_prefab, _parent);
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj = _pool.Count > 0 ? _pool.Dequeue() : CreateNew();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void Reset()
    {
        while (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            if (obj != null)
                Object.Destroy(obj.gameObject);
        }
    }
}