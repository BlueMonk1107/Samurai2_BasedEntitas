using BlueGOAP;
using UnityEngine;

namespace Game.UI
{
    public class MoveHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        private Transform _self, _enemy;
        private Rigidbody _rigidbody;

        public MoveHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {

        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入移动状态");
            _self = _agent.Maps.GetGameData(GameDataKeyEnum.SELF_TRANS) as Transform;
            _enemy = _agent.Maps.GetGameData(GameDataKeyEnum.ENEMY_TRANS) as Transform;
            _rigidbody = _self.GetComponent<Rigidbody>();
        }

        public override void Execute()
        {
            base.Execute();

            if (Vector3.Distance(_self.position, _enemy.position) <= Const.NEAR_ENEMY_DISTANCE)
            {
               OnComplete();
            }
            else
            {
                Vector3 direction = (_enemy.position - _self.position).normalized;
                _rigidbody.velocity = direction * Const.MOVE_VELOCITY;
            }
        }
    }
}
