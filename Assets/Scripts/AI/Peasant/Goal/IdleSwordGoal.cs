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
            state.Set(StateKeyEnum.IS_ALERT, true);
            state.Set(StateKeyEnum.CAN_MOVE_FORWARD, true);
            state.Set(StateKeyEnum.IS_SAFE_DISTANCE, true);
            return state;
        }

        protected override IState InitActiveCondition()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.FIND_ENEMY, true);
            state.Set(StateKeyEnum.CAN_ATTACK, false);
            state.Set(StateKeyEnum.IS_SAFE_DISTANCE, false);
            return state;
        }
    }
}
