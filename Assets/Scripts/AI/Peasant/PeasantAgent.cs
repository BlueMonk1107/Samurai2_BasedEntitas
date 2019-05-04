using System;
using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public class PeasantAgent : AgentBase<ActionEnum,GoalEnum>
    {
        public override bool IsAgentOver {
            get { return AgentState.Get(StateKeyEnum.IS_OVER.ToString()); }
        }

        private AIVIewEffectMgr _viewMgr;
        public AIVIewEffectMgr ViewMgr(IMaps<ActionEnum, GoalEnum> maps)
        {
            if (_viewMgr == null)
            {
                object audioSource = maps.GetGameData(GameDataKeyEnum.AUDIO_SOURCE);
                object self = maps.GetGameData(GameDataKeyEnum.SELF_TRANS);
                _viewMgr = new AIVIewEffectMgr(EnemyId.EnemyPeasant.ToString(), audioSource, self);
            }

            return _viewMgr;
        }

        public PeasantAgent(Action<IAgent<ActionEnum, GoalEnum>,IMaps<ActionEnum, GoalEnum>> onInitGameData) : base(onInitGameData)
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
           return new PeasantMaps(this, _onInitGameData);
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
            PeasantActMgr actMgr = ActionManager as PeasantActMgr;
            actMgr.AddExcuteNewStateListener(ViewMgr(Maps).ExcuteState);
        }
    }
}
