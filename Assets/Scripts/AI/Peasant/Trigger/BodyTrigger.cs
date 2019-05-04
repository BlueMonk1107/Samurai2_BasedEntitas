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
        protected float _height;
        protected Vector3 _hitPosition;

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
            _height = controller.height;
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
                _hitPosition = other.transform.position;
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

        protected float GetHeightValue(float scale)
        {
            float headTop = _center.y + _height * 0.5f;
            return headTop - _height * (scale / Const.ALL_BODY_SCALE);
        }
    }

    public class BodyUpTrigger : BodyTrigger
    {
        public override int Priority { get { return 1; } }

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
        public override int Priority { get { return 1; } }

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
        public override int Priority { get { return 1; } }

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
        public override int Priority { get { return 1; } }

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

    public class BodyHeadTrigger : BodyTrigger
    {
        public override int Priority { get { return 2; } }

        public BodyHeadTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_hitPosition == Vector3.zero)
                    return false;

                bool result = false;
                float headTop = GetHeightValue(0);
                float headBottom = GetHeightValue(Const.HEAD_SCALE);

                if (_hitPosition.y > headBottom && _hitPosition.y < headTop)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                _hitPosition = Vector3.zero;
                Debug.Log("头部"+result);
                SetCollideData(ActionEnum.DEAD_HALF_HEAD, result);
                return result;
            }
            set {}
        }
    }

    public class BodyBodyTrigger : BodyTrigger
    {
        public override int Priority { get { return 2; } }
        public BodyBodyTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_hitPosition == Vector3.zero)
                    return false;

                bool result = false;
                float bodyTop = GetHeightValue(Const.HEAD_SCALE);
                float bodyBottom = GetHeightValue(Const.HEAD_SCALE + Const.BODY_SCALE); 

                if (_hitPosition.y > bodyBottom && _hitPosition.y < bodyTop)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                _hitPosition = Vector3.zero;

                SetCollideData(ActionEnum.DEAD_HALF_BODY, result);
                return result;
            }
            set { }
        }
    }

    public class BodyLegTrigger : BodyTrigger
    {
        public override int Priority { get { return 2; } }

        public BodyLegTrigger(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override bool IsTrigger
        {
            get
            {
                if (_hitPosition == Vector3.zero)
                    return false;

                bool result = false;
                float legTop = GetHeightValue(Const.HEAD_SCALE + Const.BODY_SCALE);
                float legBottom = GetHeightValue(Const.ALL_BODY_SCALE);

                if (_hitPosition.y > legBottom && _hitPosition.y < legTop)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                _hitPosition = Vector3.zero;

                SetCollideData(ActionEnum.DEAD_HALF_LEG, result);
                return result;
            }
            set { }
        }
    }
}
