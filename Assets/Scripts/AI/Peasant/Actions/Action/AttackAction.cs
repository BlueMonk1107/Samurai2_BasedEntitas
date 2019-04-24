using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class AttackAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.ATTACK;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 80; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public AttackAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.CAN_ATTACK, true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.CAN_ATTACK, false);
            state.Set(StateKeyEnum.IS_SAFE_DISTANCE,false);
            return state;
        }
    }
}
