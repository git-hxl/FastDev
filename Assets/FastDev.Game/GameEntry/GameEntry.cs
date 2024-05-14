using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev.Game
{
    public partial class GameEntry : MonoSingleton<GameEntry>
    {
        private List<GameComponent> gameComponents = new List<GameComponent>();

        private void RegisterAllComponent()
        {
            UI = new UIComponent();
            gameComponents.Add(UI);

            Game = new GameComponent();
            gameComponents.Add(Game);
        }

        private void Awake()
        {
            RegisterAllComponent();
        }

        private void Start()
        {
            foreach (var item in gameComponents)
            {
                item.OnStart();
            }
        }

        private void Update()
        {
            foreach (var item in gameComponents)
            {
                item.OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var item in gameComponents)
            {
                item.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            foreach (var item in gameComponents)
            {
                item.OnFixedUpdate(Time.fixedDeltaTime);
            }
        }


        private void OnDestroy()
        {
            foreach (var item in gameComponents)
            {
                item.OnDestroy();
            }

            gameComponents.Clear();
        }
    }
}