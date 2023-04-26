namespace Framework
{
    internal interface IAudioPlayer
    {
        void PlayOnOnShot(string path);

        void Play(string path);

        void Stop();
    }
}
