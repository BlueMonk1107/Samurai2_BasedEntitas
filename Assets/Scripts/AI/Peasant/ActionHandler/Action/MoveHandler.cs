using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class MoveHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        private Transform _self, _enemy;
        private CharacterController _controller;
        private EnemyData _data;

        public MoveHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {

        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入移动状态");
            _self = _agent.Maps.GetGameData(GameDataKeyEnum.SELF_TRANS) as Transform;
            _enemy = _agent.Maps.GetGameData(GameDataKeyEnum.ENEMY_TRANS) as Transform;
            _controller = _self.GetComponent<CharacterController>();
            _data = _agent.Maps.GetGameData(GameDataKeyEnum.CONFIG) as EnemyData;
        }

        public override void Execute()
        {
            base.Execute();

            if (Vector3.Distance(_self.position, _enemy.position) <= _data.AttackRange)
            {
               OnComplete();
            }
            else
            {
                Vector3 direction = (_enemy.position - _self.position).normalized;
                _controller.SimpleMove(direction * _data.MoveSpeed);
            }
        }
    }
}
