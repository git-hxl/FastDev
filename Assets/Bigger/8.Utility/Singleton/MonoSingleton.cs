using UnityEngine;
namespace Bigger
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
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else if (instance != this)
            {
                DestroyImmediate(gameObject);
            }
        }

        protected virtual void Init()
        {

        }

        public virtual void Dispose()
        {
            instance = null;
        }
    }
}