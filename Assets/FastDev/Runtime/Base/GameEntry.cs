using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public partial class GameEntry : MonoSingleton<GameEntry>
    {
        private List<GameModule> gameModules = new List<GameModule>();

        private void RegisterAllComponent()
        {

        }

        private void Awake()
        {
            RegisterAllComponent();
        }

        private void Update()
        {
            foreach (var item in gameModules)
            {
                item.Update(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }


        private void OnDestroy()
        {
            foreach (var item in gameModules)
            {
                item.Shutdown();
            }

            gameModules.Clear();
        }
    }
}