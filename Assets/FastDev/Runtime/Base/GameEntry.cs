using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public partial class GameEntry : MonoSingleton<GameEntry>
    {
        private List<GameModule> gameModules = new List<GameModule>();

        private bool isQuit;

        private void RegisterAllComponent()
        {
            Resource = new ResourceManager();
            gameModules.Add(Resource);

            Sound = new SoundManager();
            gameModules.Add(Sound);

            UI = new UIManager();
            gameModules.Add(UI);

            Language = new LanguageManager();
            gameModules.Add(Language);

            Message = new MessageManager();
            gameModules.Add(Message);

            ObjectPool = new ObjectPoolManager();
            gameModules.Add(ObjectPool);

            WebRequest = new WebRequestManager();
            gameModules.Add(WebRequest);

            Entity = new EntityManager();
            gameModules.Add(Entity);
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

        private void OnApplicationQuit()
        {

            isQuit = true;
        }

        private void OnDestroy()
        {
            if (isQuit)
            {
                return;
            }

            foreach (var item in gameModules)
            {
                item.Shutdown();
            }

            gameModules.Clear();
        }
    }
}