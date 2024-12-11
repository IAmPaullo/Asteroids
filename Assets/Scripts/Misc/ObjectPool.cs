using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private GameObject objectPrefab;
    private Queue<T> pool = new();
    private Transform parentTransform;

    public ObjectPool(GameObject prefab, int initialPoolSize, Transform parent = null)
    {
        objectPrefab = prefab;
        parentTransform = parent;

        for (int i = 0; i < initialPoolSize; i++)
        {
            T obj = CreateObject();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private T CreateObject()
    {
        GameObject obj = Object.Instantiate(objectPrefab, parentTransform);
        return obj.GetComponent<T>();
    }

    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("Pool is empty!");
            return CreateObject();
        }
    }

    public void ReturnObject(T obj, Transform parent = null)
    {
        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

}
