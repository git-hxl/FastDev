using UnityEngine;
namespace Framework

{
    public abstract class MonoSingleton<T> : MonoBehaviour, IDispose where T : MonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError(typeof(T).Name + " is Null!!!");
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

        public void Dispose()
        {
            instance = null;
            DestroyImmediate(gameObject);
        }
    }
}