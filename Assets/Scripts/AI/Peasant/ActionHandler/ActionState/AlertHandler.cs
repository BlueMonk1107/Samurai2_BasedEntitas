using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class AlertHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        private Transform _self, _enemy;
        private EnemyData _data;

        public AlertHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入警戒状态");
            _self = _agent.Maps.GetGameData(GameDataKeyEnum.SELF_TRANS) as Transform;
            _enemy = _agent.Maps.GetGameData(GameDataKeyEnum.ENEMY_TRANS) as Transform;
            _data = GetGameData(GameDataKeyEnum.CONFIG) as EnemyData;
        }

        public override void Execute()
        {
            base.Execute();
            
            if (_data.Life > 0 && _agent.AgentState.ContainState(Action.Preconditions))
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
