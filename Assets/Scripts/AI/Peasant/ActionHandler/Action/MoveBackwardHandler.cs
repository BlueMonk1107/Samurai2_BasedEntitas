using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public class MoveBackwardHandler : HandlerBase<IModel>
    {
        private Transform _self, _enemy;
        private CharacterController _controller;
        private EnemyData _data;

        public MoveBackwardHandler(IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action) 
            : base(agent,maps, action)
        {
            _self = GetGameData<GameDataKeyEnum, Transform>(GameDataKeyEnum.SELF_TRANS);
            _enemy = GetGameData<GameDataKeyEnum, Transform>(GameDataKeyEnum.ENEMY_TRANS);
            _controller = _self.GetComponent<CharacterController>();
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入后退状态");
            _data = GetGameData<GameDataKeyEnum, EnemyData>(GameDataKeyEnum.CONFIG);
        }

        public override void Execute()
        {
            base.Execute();
            if (Vector3.Distance(_self.position, _enemy.position) >= _data.SafeDistance)
            {
                OnComplete();
            }
            else
            {
                Vector3 direction = (_self.position - _enemy.position).normalized;
                _controller.SimpleMove(direction * _data.MoveSpeed);
            }
        }
    }
}
