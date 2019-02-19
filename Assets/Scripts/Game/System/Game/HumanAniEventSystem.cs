using System;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 初始化人物行为函数
    /// </summary>
    public class GameHumanAniEventSystem : IInitializeSystem      
    {
        protected Contexts _contexts;

        public  GameHumanAniEventSystem(Contexts context)         
        {
            _contexts = context;
        }
           
        public async void Initialize()
        {
            await Task.Delay(800);
            ICustomAniEventManager manager = _contexts.game.gamePlayer.Ani.AniEventManager;
            manager.AddEventListener(Enter, Update, Exit);
        }

        private void Enter(string name)
        {
            RepalceData(name, BehaviorState.ENTER);
        }

        private void Update(string name)
        {
            RepalceData(name, BehaviorState.UPDATE);
        }

        private void Exit(string name)
        {
            RepalceData(name, BehaviorState.EXIT);
        }

        private void RepalceData(string name, BehaviorState state)
        {
            foreach (PlayerBehaviourIndex behaviourIndex in Enum.GetValues(typeof(PlayerBehaviourIndex)))
            {
                RepalceData(name, behaviourIndex, state);
            }
        }

        private void RepalceData(string name,PlayerBehaviourIndex behaviour, BehaviorState state)
        {
            string key = behaviour.ToString().ToLower();
            if (name.Contains(key))
            {
                _contexts.game.ReplaceGameHumanBehaviourState(behaviour, state);
            }
        }
    }
}
