using System.Collections.Generic;
using BlueGOAP;
using Const;
using UnityEngine;

namespace Game.AI
{
    public abstract class BodyTrigger : TriggerBase
    {
        protected Vector3 _center;
        protected Vector3 _direction;

        public BodyTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
            InitCenter();
            InitUnityTrigger();
        }

        private void InitCenter()
        {
            var self = GetGameData<Transform>(GameDataKeyEnum.SELF_TRANS);
            var controller = self.GetComponent<CharacterController>();
            _center = controller.center;
        }

        private void InitUnityTrigger()
        {
            var unityTrigger = GetGameData<UnityTrigger>(GameDataKeyEnum.UNITY_TRIGGER);
            if (unityTrigger != null)
            {
                unityTrigger.AddCollideListener(Collider);
            }
        }

        private void Collider(Collider other)
        {
            if (other.tag == TagAndLayer.WEAPON_TAG)
            {
                _direction = (other.transform.position - _center).normalized;
                _direction.z = 0;
            }
        }

        //获取到当前碰撞体的中心
        //获取到武器和碰撞体的第一个接触点
        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_INJURE, true);
            return state;
        }

        protected void SetCollideData(ActionEnum label,bool result)
        {
            var dic = GetGameData<Dictionary<ActionEnum, bool>>(GameDataKeyEnum.INJURE_COLLODE_DATA);
            dic[label] = result;
        }
    }

    public class BodyUpTrigger : BodyTrigger
    {
        public BodyUpTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_direction == Vector3.zero)
                    return false;

                var result = Vector3.Angle(Vector3.up, _direction) < Const.BODY_PART_RANGE;
                SetCollideData(ActionEnum.INJURE_UP, result);
                _direction = Vector3.zero;
                return result;
            }
            set { }
        }
    }

    public class BodyDownTrigger : BodyTrigger
    {
        public BodyDownTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_direction == Vector3.zero)
                    return false;

                var result = Vector3.Angle(Vector3.down, _direction) < Const.BODY_PART_RANGE;
                SetCollideData(ActionEnum.INJURE_DOWN, result);
                _direction = Vector3.zero;
                return result;
            }
            set { }
        }
    }

    public class BodyLeftTrigger : BodyTrigger
    {
        public BodyLeftTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_direction == Vector3.zero)
                    return false;

                var result = Vector3.Angle(Vector3.left, _direction) < 90 - Const.BODY_PART_RANGE;
                SetCollideData(ActionEnum.INJURE_LEFT, result);
                _direction = Vector3.zero;
                return result;
            }
            set { }
        }
    }

    public class BodyRightTrigger : BodyTrigger
    {
        public BodyRightTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_direction == Vector3.zero)
                    return false;

                var result = Vector3.Angle(Vector3.right, _direction) < 90 - Const.BODY_PART_RANGE;
                SetCollideData(ActionEnum.INJURE_RIGHT, result);
                _direction = Vector3.zero;
                return result;
            }
            set { }
        }
    }
}
