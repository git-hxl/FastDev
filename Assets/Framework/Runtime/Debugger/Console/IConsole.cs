
using System;
using System.Collections.Generic;

namespace Framework
{
    public interface IConsole
    {
        Dictionary<string, Func<object[], string>> Commands { get; }
        void RegisterCommand(string command, Func<object[], string> action);

        void UnregisterCommand(string command);
        string Execute(string command, params object[] parameters);
    }
}