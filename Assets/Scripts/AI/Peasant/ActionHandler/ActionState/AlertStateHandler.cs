using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public class AlertStateHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        private Transform _self, _enemy;
        private EnemyData _data;

        public AlertStateHandler(
            IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action)
            : base(agent, maps, action)
        {
            _self = GetGameData<GameDataKeyEnum, Transform>(GameDataKeyEnum.SELF_TRANS);
            _enemy = GetGameData<GameDataKeyEnum, Transform>(GameDataKeyEnum.ENEMY_TRANS);
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入警戒状态");
            _data = GetGameData<GameDataKeyEnum, EnemyData>(GameDataKeyEnum.CONFIG);
        }

        public override void Execute()
        {
            if(ExcuteState == ActionExcuteState.EXIT)
                return;

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
