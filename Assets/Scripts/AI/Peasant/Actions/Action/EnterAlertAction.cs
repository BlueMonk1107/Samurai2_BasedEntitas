using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class EnterAlertAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get {return ActionEnum.ENTER_ALERT;} }
        public override int Cost { get { return 10; } }
        public override int Priority { get { return 90; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public EnterAlertAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.FIND_ENEMY, true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_ALERT, true);
            return state;
        }
    }
}
