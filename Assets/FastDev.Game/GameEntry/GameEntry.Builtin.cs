
namespace FastDev.Game
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

        public static UIComponent UI
        {
            get;
            private set;
        }

        public static GameComponent Game
        {
            get;
            private set;
        }
    }
}