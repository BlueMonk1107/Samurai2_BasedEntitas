using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class InjureAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.INJJURE;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 100; } }
        public override bool CanInterruptiblePlan { get { return true; } }

        public InjureAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.INJURE, true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.INJURE, false);
            return state;
        }
    }
}
