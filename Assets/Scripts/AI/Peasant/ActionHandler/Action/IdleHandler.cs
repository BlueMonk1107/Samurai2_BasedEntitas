using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class IdleHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        public IdleHandler(IAgent<ActionEnum, GoalEnum> agent, 
            IMaps<ActionEnum, GoalEnum> maps, 
            IAction<ActionEnum> action)
            : base(agent,maps, action)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入待机状态");
        }

        public override void Execute()
        {
            base.Execute();

            if (_agent.AgentState.Get(StateKeyEnum.FIND_ENEMY.ToString()) == true)
            {
                OnComplete();
            }
        }
    }
}
