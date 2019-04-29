using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class EyesTrigger : TriggerBase
    {
        private Transform _self, _enemy;

        public EyesTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
            _self = GetGameData<Transform>(GameDataKeyEnum.SELF_TRANS);
            _enemy = GetGameData<Transform>(GameDataKeyEnum.ENEMY_TRANS);
        }

        public override bool IsTrigger {
            get
            {
                if (_enemy == null || _self == null)
                    return false;

                EnemyData data = GetGameData<EnemyData>(GameDataKeyEnum.CONFIG);
                //比对发现目标的距离
                if (Vector3.Distance(_self.position, _enemy.position) < data.FindDistance)
                {
                    //查看是否在视线角度内
                    Vector3 dirToEnemy = (_enemy.position - _self.position).normalized;
                    if (Vector3.Angle(_self.forward, dirToEnemy) < Const.SIGHT_LINE_RANGE)
                    {
                        return true;
                    }
                }

                return false;
            }
            set { }
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.FIND_ENEMY, true);
            return state;
        }
    }
}
