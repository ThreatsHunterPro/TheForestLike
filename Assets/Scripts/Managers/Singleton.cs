using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance => instance;

    private void Awake() => Init();

    private void Init()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        name += " [MANAGER]";
        instance = this as T;
    }
}