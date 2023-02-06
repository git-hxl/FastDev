namespace FastDev
{
    public interface IInputManager
    {
        bool InputEnabled { get; set; }

        bool GetKeyDown(string id);

        bool GetKey(string id);

        bool GetKeyUp(string id);

        float GetAxis(string id);
        float GetAxisRaw(string id);

        void RegisterAction(string id, InputData action);

        void Save();
    }
}
