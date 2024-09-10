using UnityEngine;
namespace FastDev

{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogError(typeof(T).Name + " is Null");
                    }
                    else
                    {
                        instance.Init();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                Init();
            }
            else if (instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"{typeof(T).Name} {gameObject.name} Init!");
            OnInit();
        }

        protected virtual void OnInit() { }

        public void Dispose()
        {
            instance = null;
            OnDispose();
            Destroy(gameObject);
        }

        protected virtual void OnDispose() { }
    }
}