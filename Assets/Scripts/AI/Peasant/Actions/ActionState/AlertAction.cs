using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class AlertAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.ALERT; } }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 0; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public AlertAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {

        }

        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.FIND_ENEMY,true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.FIND_ENEMY, false);
            return state;
        }
    }
}
