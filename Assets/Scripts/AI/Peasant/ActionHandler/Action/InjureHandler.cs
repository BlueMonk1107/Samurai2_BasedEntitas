using System.Collections.Generic;
using BlueGOAP;
using Game.AI.ViewEffect;

namespace Game.AI
{
    public class InjureHandler : HandlerBase<InjureModel>
    {
        public InjureHandler(IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action) 
            : base(agent,maps, action)
        {
        }

        public override void Enter()
        {
            DebugMsg.Log("进入受伤状态："+Label);
            int injureValue = GetGameDataValue<int>(GameDataKeyEnum.INJURE_VALUE);
            EnemyData data = GetGameData<EnemyData>(GameDataKeyEnum.CONFIG);

            data.Life = data.Life - injureValue;
            if (data.Life <= 0)
            {
                SetAgentState(StateKeyEnum.IS_DEAD, true);
            }
            else
            {
                base.Enter();
            }
        }

        public override bool CanPerformAction()
        {
            var dataDic = GetGameData<Dictionary<ActionEnum, bool>>(GameDataKeyEnum.INJURE_COLLODE_DATA);
            bool result = base.CanPerformAction() && dataDic.ContainsKey(Label) && dataDic[Label];
            dataDic[Label] = false;
            ChangeActionPriority(result);
            return result;
        }

        private void ChangeActionPriority(bool isChange)
        {
            ((InjureAction)Action).ChangePriority(isChange);
        }
    }
}
