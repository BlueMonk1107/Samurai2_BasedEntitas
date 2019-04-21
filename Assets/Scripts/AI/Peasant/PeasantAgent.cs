using System;
using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class PeasantAgent : AgentBase<ActionEnum,GoalEnum>
    {

        public PeasantAgent() : base() { }

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
    }
}
