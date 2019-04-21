using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class IdleSwordGoal : GoalBase<ActionEnum, GoalEnum>
    {
        public override GoalEnum Label { get { return GoalEnum.IDLE_SWORD;} }

        public IdleSwordGoal(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        public override float GetPriority()
        {
            return 0;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.MOVE, true);
            return state;
        }

        protected override bool ActiveCondition()
        {
            return GetAgentState(StateKeyEnum.ALERT) == true
                && GetAgentState(StateKeyEnum.ATTACK) == false
                && GetAgentState(StateKeyEnum.MOVE) == false; 
        }
    }
}
