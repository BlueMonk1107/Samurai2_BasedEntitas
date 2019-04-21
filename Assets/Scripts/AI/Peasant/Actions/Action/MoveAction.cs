using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class MoveAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.MOVE;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 10; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public MoveAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.MOVE,true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.NEAR_ENEMY, true);
            return state;
        }
    }
}
