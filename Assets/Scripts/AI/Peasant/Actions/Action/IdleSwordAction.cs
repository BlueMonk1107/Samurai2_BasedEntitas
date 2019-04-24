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
            state.Set(StateKeyEnum.FIND_ENEMY, true);
            state.Set(StateKeyEnum.IS_SAFE_DISTANCE, true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.CAN_MOVE_FORWARD, true);
            state.Set(StateKeyEnum.CAN_ATTACK, true);
            return state;
        }
    }
}
