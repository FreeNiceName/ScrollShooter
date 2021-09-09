using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _quitting = false;
    private static object _locker = new object();
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_quitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString();

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
    }


    private void OnApplicationQuit()
    {
        _quitting = true;
    }


    private void OnDestroy()
    {
        _quitting = true;
    }
}