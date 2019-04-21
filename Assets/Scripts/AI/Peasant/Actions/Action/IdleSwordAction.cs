using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class IdleSwordAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override ActionEnum Label { get {return ActionEnum.IDLE_SWORD;} }
        public override int Cost { get { return 1; } }
        public override int Priority { get { return 1; } }
        public override bool CanInterruptiblePlan { get { return false; } }

        public IdleSwordAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
            
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.ALERT, true);
            state.Set(StateKeyEnum.ATTACK, false);
            state.Set(StateKeyEnum.MOVE, false);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.MOVE, true);
            return state;
        }
    }
}
