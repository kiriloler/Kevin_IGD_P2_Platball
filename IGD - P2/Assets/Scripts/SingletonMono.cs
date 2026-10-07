using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T: SingletonMono<T>
{
    public static T Instance;
    protected virtual void Awake()
    {
        if(Instance == null)
        {
            Instance = (T)this;
            Debug.Log(Instance);
        }
    }
}
