using UnityEngine;
namespace FastDev
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        private bool isInit = false;
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
                        //Awake
                        obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (!isInit)
            {
                isInit = true;
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        protected virtual void Init()
        {
            Debug.Log(typeof(T).Name + " Init!");
        }

        public virtual void Dispose()
        {
            instance = null;
        }
    }
}