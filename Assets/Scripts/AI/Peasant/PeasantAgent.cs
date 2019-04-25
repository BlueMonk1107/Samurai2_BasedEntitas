using System;
using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public class PeasantAgent : AgentBase<ActionEnum,GoalEnum>
    {

        public PeasantAgent() : base()
        {
            InitViewMgr();
        }

        protected override IState InitAgentState()
        {
            State< StateKeyEnum > state = new State<StateKeyEnum>();

            foreach (StateKeyEnum key in Enum.GetValues(typeof(StateKeyEnum)))
            {
                state.Set(key,false);
            }
            return state;
        }

        protected override IMaps<ActionEnum, GoalEnum> InitMaps()
        {
           return new PeasantMaps(this);
        }

        protected override IActionManager<ActionEnum> InitActionManager()
        {
            return new PeasantActMgr(this);
        }

        protected override IGoalManager<GoalEnum> InitGoalManager()
        {
            return new PeasantGoalMgr(this);
        }

        protected override ITriggerManager InitTriggerManager()
        {
            return new PeasasntTriggerMgr(this);
        }

        protected override DebugMsgBase InitDebugMsg()
        {
            return new AIDebug();
        }

        private void InitViewMgr()
        {
            object audioSource = Maps.GetGameData(GameDataKeyEnum.AUDIO_SOURCE);
            AIVIewEffectMgr viewMgr = new AIVIewEffectMgr(EnemyId.EnemyPeasant.ToString(),audioSource); 

            PeasantActMgr actMgr = ActionManager as PeasantActMgr;
            actMgr.AddExcuteNewStateListener(viewMgr.ExcuteState);
        }
    }
}
