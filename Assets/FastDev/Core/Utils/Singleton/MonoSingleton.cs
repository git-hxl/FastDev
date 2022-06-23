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
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        public virtual void Dispose()
        {
            instance = null;
            Destroy(gameObject);
        }
    }
}