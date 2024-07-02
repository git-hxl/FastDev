namespace FastDev
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T instance;
        private static object locker = new object();
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                            instance.OnInit();
                        }
                    }
                }
                return instance;
            }
        }

        protected virtual void OnInit() { }

        public void Dispose()
        {
            instance = null;
            OnDispose();
        }

        protected virtual void OnDispose() { }
    }
}