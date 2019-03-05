using System;
using UnityEngine;

namespace Game
{
    public class GameFeature : Feature, IFeature 
    {
        public GameFeature(Contexts contexts)
        {
            InitializeFun(contexts);
            ExecuteFun(contexts);
            CleanupFun(contexts);
            TearDownFun(contexts);
            ReactiveSystemFun(contexts);
        }

        public void InitializeFun(Contexts contexts)
        {
            Add(new GameSkillManagerSystem(contexts));
            Add(new GameInitGameSystem(contexts));
            Add(new GameHumanAniEventSystem(contexts));
        }

        public void ExecuteFun(Contexts contexts)
        {
        }

        public void CleanupFun(Contexts contexts)
        {
        }

        public void TearDownFun(Contexts contexts)
        {
        }

        public void ReactiveSystemFun(Contexts contexts)
        {
            Add(new GamePlayHumanSkillSystem(contexts));
            Add(new GameStartSystem(contexts));
            Add(new GamePauseSystem(contexts));
            Add(new GameEndSystem(contexts));
            Behaviour(contexts);
        }

        private void Behaviour(Contexts contexts)
        {
            //enter
            Add(new GameEnterIdelStateSystem(contexts));
            Add(new GameEnterWalkStateSystem(contexts));
            Add(new GameEnterAttackStateSystem(contexts));
            //update
            Add(new GameUpdateMoveStateSystem(contexts));
        }
    }
}
