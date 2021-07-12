using UnityEngine;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance {
        get { return instance; }
        set {
            if (instance == null)
            {
                instance = value;
                DontDestroyOnLoad(instance.gameObject);
            }
            else if (instance != value)
                Destroy(value.gameObject);
        }
    }
    private void Awake()
    {
        Instance = this as T;
    }
}
