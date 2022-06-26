namespace FastDev
{
    public interface IAudioManager 
    {
        void PlayMusic(string path);
        void StopMusic();

        void PlaySound(string path);
    }
}