using UnityEngine;
namespace FastDev
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        private static bool onApplicationQuit = false;
        public static T Instance
        {
            get
            {
                if (instance == null && onApplicationQuit == false)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        public static bool isNull { get { return instance == null; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
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
            instance = null;
            Destroy(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            onApplicationQuit = true;
        }
    }
}