using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class InjureGoal : GoalBase<ActionEnum, GoalEnum>
    {
        public override GoalEnum Label { get {return GoalEnum.INJJURE;} }

        public InjureGoal(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

      
        public override float GetPriority()
        {
            return 60;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_INJURE, false);
            return state;
        }

        protected override IState InitActiveCondition()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_INJURE, true);
            return state;
        }
    }
}
