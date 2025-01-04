using UnityEngine;
using UnityEngine.Pool;

public class MonoBehaviourPool<T> : IObjectPool<T> where T : MonoBehaviour, IPoolable
{
    IObjectPool<T> pool;
        
    public MonoBehaviourPool(T prefab)
    {
        pool = new ObjectPool<T>(
            createFunc: () => Object.Instantiate(prefab, Vector3.zero, Quaternion.identity),
            actionOnGet: item => item.gameObject.SetActive(true),
            actionOnRelease: item => item.gameObject.SetActive(false),
            actionOnDestroy: item => Object.Destroy(item.gameObject),
            collectionCheck: true, 
            defaultCapacity: 1,
            maxSize: 1000);
    }
    
    public int CountInactive => pool.CountInactive;

    public T Get()
    {
        return pool.Get();
    }

    public PooledObject<T> Get(out T v)
    {
        return pool.Get(out v);
    }

    public void Release(T element)
    {
        element.OnReleased();
        pool.Release(element);
    }

    public void Clear()
    {
        pool.Clear();
    }
}

public interface IPoolable
{
    public void OnReleased();
}
