using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class DeadAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.DEAD;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 1000; } }
        public override bool CanInterruptiblePlan { get { return true; } }

        public DeadAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_DEAD, true);
            return state;
        }

        protected override IState InitEffects()
        {
            return null;
        }
    }
}
