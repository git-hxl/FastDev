
namespace Framework
{
    public interface IConsoleWindow
    {
        void DrawWindow(int id);

        void DrawInput();

        void DrawOutput();

        void BackToLastInput();

        void ClearOutput();
    }
}
