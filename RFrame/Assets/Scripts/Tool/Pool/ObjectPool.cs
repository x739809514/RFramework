using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private GameObject prefab;
    private Queue<T> objectPool;
    private readonly int RETAIL_COUNT = 5;
    private Transform parent;

#region 单例模式

    private static ObjectPool<T> _instance;

    public static ObjectPool<T> Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPool<T>();
            }

            return _instance;
        }
    }

#endregion

    public void InitPool(Transform parent, GameObject prefab)
    {
        this.parent = parent;
        this.prefab = prefab;
        objectPool = new Queue<T>();
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Object.Instantiate(prefab, this.parent, true);
            go.SetActive(false);
            var t = go.GetComponent<T>();
            objectPool.Enqueue(t);
        }
    }

    public T Spawn()
    {
        if (objectPool.Count == 0)
        {
            CreatePool(RETAIL_COUNT);
        }

        var t = objectPool.Dequeue();
        var go = t.gameObject;
        go.SetActive(true);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        return t;
    }

    public void Recycle(T t)
    {
        t.gameObject.SetActive(false);
        objectPool.Enqueue(t);
    }
}