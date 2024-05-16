
namespace FastDev
{
    public partial class GameEntry
    {
        public static ResourceManager Resource
        {
            get;
            private set;
        }

        public static SoundManager Sound
        {
            get;
            private set;
        }

        public static UIManager UI
        {
            get;
            private set;
        }

        public static MessageManager Message
        {
            get;
            private set;
        }

        public static ObjectPoolManager ObjectPool
        {
            get;
            private set;
        }

        public static LanguageManager Language
        {
            get;
            private set;
        }

        public static WebRequestManager WebRequest
        {
            get;
            private set;
        }

        public static EntityManager Entity
        {
            get;
            private set;
        }
    }
}