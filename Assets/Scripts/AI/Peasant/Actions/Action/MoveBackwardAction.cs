using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class MoveBackwardAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.MOVE_BACKWARD;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 8; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public MoveBackwardAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.STEP_BACK, true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.NEAR_ENEMY, false);
            return state;
        }
    }
}
