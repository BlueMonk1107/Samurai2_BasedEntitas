using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class DeadHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        public DeadHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
        }

        public override void Enter()
        {
            base.Enter();

            int injureValue = (int)GetGameData(GameDataKeyEnum.INJURE_VALUE);
            

            switch (injureValue)
            {
                case Const.INTANT_KILL_VALUE:
                    //todo：一击必杀效果
                    break;
                default:
                    //todo：正常死亡逻辑
                    break; 
            }
        }


    }
}
