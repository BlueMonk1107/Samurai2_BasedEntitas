using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class InjureHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        public InjureHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入受伤状态");
            int injureValue = (int) GetGameData(GameDataKeyEnum.INJURE_VALUE);
            EnemyData data = GetGameData(GameDataKeyEnum.CONFIG) as EnemyData;

            data.Life = data.Life - injureValue;

            if (data.Life < 0)
            {
                SetAgentState(StateKeyEnum.DEAD, true);
            }

            //todo:动画执行完毕，执行Oncomplete
            OnComplete();
        }
    }
}
