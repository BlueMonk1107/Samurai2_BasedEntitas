using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class DeadGoal : GoalBase<ActionEnum, GoalEnum>
    {
        public override GoalEnum Label { get {return GoalEnum.DEAD;} }

        public DeadGoal(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }


        public override float GetPriority()
        {
            return 100;
        }

        protected override IState InitEffects()
        {
            return null;
        }

        protected override bool ActiveCondition()
        {
            return GetAgentState(StateKeyEnum.DEAD) == true;
        }
    }
}
