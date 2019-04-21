using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class AttackHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        public AttackHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入攻击状态");
            //todo:执行动画，播放声音
            OnComplete();
        }
    }
}
