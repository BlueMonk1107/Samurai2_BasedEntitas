using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public class DeadHandler : HandlerBase<IModel>
    {
        public DeadHandler(IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action)
            : base(agent, maps,action)
        {
        }

        public override void Enter()
        {
            base.Enter();

            int injureValue = GetGameDataValue<GameDataKeyEnum,int>(GameDataKeyEnum.INJURE_VALUE);
            

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
