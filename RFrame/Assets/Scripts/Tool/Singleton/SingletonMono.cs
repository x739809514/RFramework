using System;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private SingletonMono<T> instance;

    public SingletonMono<T> Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = (T)this;
    }
}