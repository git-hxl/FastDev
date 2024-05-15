
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
    }
}