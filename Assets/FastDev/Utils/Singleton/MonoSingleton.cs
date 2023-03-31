using UnityEngine;
namespace FastDev
{
    public abstract class MonoSingleton<T> : MonoBehaviour, IDispose where T : MonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null && Application.isPlaying)
                {
                    new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                Debug.Log(typeof(T).Name + " Init!");
                OnInit();
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        protected virtual void OnInit() { }

        public virtual void Dispose()
        {
            instance = null;
            Destroy(gameObject);
        }
    }
}