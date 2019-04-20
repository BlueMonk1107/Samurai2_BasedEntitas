using BlueGOAP;
using UnityEngine;

namespace Game.UI
{
    public class AlertHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        private Transform _self, _enemy;

        public AlertHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入警戒状态");
            _self = _agent.Maps.GetGameData(GameDataKeyEnum.SELF_TRANS) as Transform;
            _enemy = _agent.Maps.GetGameData(GameDataKeyEnum.ENEMY_TRANS) as Transform;
        }

        public override void Execute()
        {
            base.Execute();

            int life = (int)GetGameData(GameDataKeyEnum.LIFE);
            if (life > 0 && _agent.AgentState.ContainState(Action.Preconditions))
            {
                _self.LookAt(_enemy);
            }
            else
            {
                OnComplete();
            }
        }
    }
}
