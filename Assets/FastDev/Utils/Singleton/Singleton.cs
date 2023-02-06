namespace FastDev
{
    public abstract class Singleton<T> where T : class, new()
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
                        }
                    }
                }
                return instance;
            }
        }
        public virtual void Dispose()
        {
            instance = null;
        }
    }
}