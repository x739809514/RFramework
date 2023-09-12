using System;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;

    public static T Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = (T)this;
    }
}