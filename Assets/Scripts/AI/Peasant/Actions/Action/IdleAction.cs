using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class IdleAction : ActionBase<ActionEnum,GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.IDLE;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 1; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public IdleAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.ALERT, false);
            state.Set(StateKeyEnum.ATTACK, false);
            state.Set(StateKeyEnum.MOVE, false);
            return state;
        }

        protected override IState InitEffects()
        {
            return null;
        }
    }
}
