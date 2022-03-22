using UnityEngine;
namespace FastDev
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static bool onApplicationQuit = false;
        private static T _instance = null;
        public static T instance
        {
            get
            {
                if (_instance == null && onApplicationQuit == false)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance != null)
                    {
                        _instance.Init();
                    }
                    else
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        public static bool isNull { get { return _instance == null; } }

        private void Init()
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            OnInit();
            Debug.Log(typeof(T).Name + " Init!");
        }

        protected virtual void OnInit()
        {

        }

        private void Awake()
        {
            if (_instance == null)
            {
                Init();
            }
            else if (_instance != this)
            {
                DestroyImmediate(this);
            }
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