using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains basic functionality to pool objects
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{

    /// <summary>
    /// The object that is spawned in this pool
    /// </summary>
    [SerializeField]
    T objectToSpawn;

    /// <summary>
    /// The object that should be the parent of all spawned objects
    /// </summary>
    [SerializeField]
    GameObject parentObject;

    /// <summary>
    /// Initial number of objects in the pool
    /// </summary>
    [SerializeField]
    int initialSize;

    /// <summary>
    /// True if the object pool should increase from the initial size
    /// if all objects in the pool are currently active
    /// </summary>
    [SerializeField]
    bool shouldIncreasePoolIfEmpty;

    /// <summary>
    /// True if the object pool should increase from the initial size
    /// if all objects in the pool are currently active
    /// </summary>
    /// <value></value>
    public bool ShouldIncreasePoolIfEmpty
    {
        get
        {
            return shouldIncreasePoolIfEmpty;
        }
    }

    /// <summary>
    /// Keeps track of all objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    List<T> objectPool = new List<T>();

    /// <summary>
    /// Adds an object to the object pool, returning the added object
    /// </summary>
    /// <returns></returns>
    T AddObjectToPool()
    {
        T temp;
        if (parentObject != null)
        {
            temp = Instantiate(objectToSpawn, parentObject.transform);
        }
        else
        {
            temp = Instantiate(objectToSpawn);
        }
        temp.gameObject.SetActive(false);

        objectPool.Add(temp);

        return temp;
    }

    public List<T> GetActiveObjects()
    {
        List<T> toReturn = new List<T>();
        foreach (T item in objectPool)
        {
            if (item.gameObject.activeInHierarchy)
            {
                toReturn.Add(item);
            }
        }

        return toReturn;
    }

    /// <summary>
    /// Initializes the pool with objects
    /// </summary>
    void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns>An object from the pool, or null, if the pool size is restricted and all objects are currently in use</returns>
    public T GetObject()
    {
        foreach (T obj in objectPool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }

        return shouldIncreasePoolIfEmpty ? AddObjectToPool() : null;
    }
}
