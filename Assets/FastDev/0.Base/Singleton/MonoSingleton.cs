using UnityEngine;
namespace FastDev
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance = null;
        private static bool onApplicationQuit = false;
        public static T instance
        {
            get
            {
                if (_instance == null && onApplicationQuit == false)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        public static bool isNull { get { return _instance == null; } }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else
                DestroyImmediate(gameObject);
        }

        protected virtual void Init()
        {
            Debug.Log(typeof(T).Name + " Init!");
        }

        public virtual void Dispose()
        {
            _instance = null;
            Destroy(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            onApplicationQuit = true;
        }
    }
}