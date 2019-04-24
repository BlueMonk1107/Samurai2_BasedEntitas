using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class MoveBackwardAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.MOVE_BACKWARD;} }
        public override int Cost { get { return 5; } }
        public override int Priority { get { return 8; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public MoveBackwardAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.CAN_MOVE_FORWARD, false);
            state.Set(StateKeyEnum.IS_SAFE_DISTANCE, false);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_SAFE_DISTANCE, true);
            return state;
        }
    }
}
