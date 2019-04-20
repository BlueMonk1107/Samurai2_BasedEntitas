using BlueGOAP;
using UnityEngine;

namespace Game.UI
{
    public class EyesTrigger : TriggerBase<ActionEnum, GoalEnum>
    {
        private Transform _self, _enemy;

        public EyesTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
            _self = agent.Maps.GetGameData(GameDataKeyEnum.SELF_TRANS) as Transform;
            _enemy = agent.Maps.GetGameData(GameDataKeyEnum.ENEMY_TRANS) as Transform;
        }

        public override bool IsTrigger {
            get
            {
                if (_enemy == null || _self == null)
                    return false;
                //比对发现目标的距离
                if (Vector3.Distance(_self.position, _enemy.position) < Const.FIND_DISTANCE)
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
