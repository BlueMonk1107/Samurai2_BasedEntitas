using System.Collections.Generic;
using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public abstract class DeadHandler : HandlerBase<DeadModel>
    {
        public DeadHandler(IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action)
            : base(agent, maps,action)
        {
        }

        public override bool CanPerformAction()
        {
            bool result = base.CanPerformAction() && JudgeDead();
            ((DeadAction)Action).ChangePriority(result);
            return result;
        }

        protected abstract bool JudgeDead();
    }
    /// <summary>
    /// 普通死亡方式
    /// </summary>
    public class DeadNormalHandler : DeadHandler
    {
        public DeadNormalHandler(
            IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps, 
            IAction<ActionEnum> action) 
            : base(agent, maps, action)
        {
        }

        protected override bool JudgeDead()
        {
            return true;
        }
    }
    /// <summary>
    /// 一击必杀死亡方式
    /// </summary>
    public class DeadIntantKillHandler : DeadHandler
    {
        public DeadIntantKillHandler(
            IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action)
            : base(agent, maps, action)
        {
        }

        protected override bool JudgeDead()
        {
            int injureValue = GetGameDataValue<int>(GameDataKeyEnum.INJURE_VALUE);
            var dataDic = GetGameData<Dictionary<ActionEnum, bool>>(GameDataKeyEnum.INJURE_COLLODE_DATA);
            bool result = dataDic.ContainsKey(Label) && dataDic[Label] && injureValue == Const.INTANT_KILL_VALUE;
            dataDic[Label] = false;
            return result;
        }
    }
}
