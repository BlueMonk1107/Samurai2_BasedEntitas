using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class AttackGoal : GoalBase<ActionEnum, GoalEnum>
    {
        public override GoalEnum Label { get {return GoalEnum.ATTACK;} }

        public AttackGoal(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        public override float GetPriority()
        {
            return 40;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.ALERT, true);
            state.Set(StateKeyEnum.ATTACK, false);
            state.Set(StateKeyEnum.MOVE, false);
            return state;
        }

        protected override bool ActiveCondition()
        {
            return GetAgentState(StateKeyEnum.FIND_ENEMY) == true
                && GetAgentState(StateKeyEnum.MOVE) == false
                && GetAgentState(StateKeyEnum.STEP_BACK) == false;
        }
    }
}
