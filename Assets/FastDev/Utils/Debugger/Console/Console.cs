
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class Console : Singleton<Console>, IConsole
    {
        public Dictionary<string, Func<object[], string>> Commands { get; private set; } = new Dictionary<string, Func<object[], string>>();

        public string Execute(string command, params object[] parameters)
        {
            try
            {
                command = command.ToUpperInvariant();
                if (Commands.ContainsKey(command))
                {
                    return Commands[command].Invoke(parameters);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "invalid command";
        }

        public void RegisterCommand(string command, Func<object[], string> action)
        {
            command = command.ToUpperInvariant();
            Commands[command] = action;
        }

        public void UnregisterCommand(string command)
        {
            command = command.ToUpperInvariant();
            if (Commands.ContainsKey(command))
            {
                Commands.Remove(command);
            }
        }
    }
}
