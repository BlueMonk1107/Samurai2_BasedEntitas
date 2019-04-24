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
            state.Set(StateKeyEnum.CAN_ATTACK, false);
            state.Set(StateKeyEnum.CAN_MOVE_FORWARD, false);
            return state;
        }

        protected override IState InitActiveCondition()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.CAN_ATTACK, true);
            state.Set(StateKeyEnum.CAN_MOVE_FORWARD, true);
            return state;
        }
    }
}
