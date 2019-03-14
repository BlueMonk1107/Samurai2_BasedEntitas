using Const;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform _player;
        private Vector3 _offset;
        private Vector3 _lastPos;
        private bool _isMoving;
        private float _rotateValue;
        private Vector3 _defaultEul;
        private float _rotateDuration;

        public void Start()
        {
            _defaultEul = transform.eulerAngles;
            _rotateValue = 1f;
            _isMoving = false;
            _rotateDuration = 0.8f;
            _lastPos = transform.position;
            _player = GameObject.FindGameObjectWithTag(TagAndLayer.PLAYER_TAG).transform;
            Transform playerRoot = _player.parent;
            _offset = transform.position - playerRoot.position;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _lastPos) <0.01f && _isMoving)
            {
                //移动结束
                _isMoving = false;

                transform.DORotate(_defaultEul, _rotateDuration);
            }
            else if (Vector3.Distance(transform.position, _lastPos) >= 0.01f && !_isMoving)
            {
                //移动开始
                _isMoving = true;

                int directionX = GetXDirection();
                int directionZ = GetZDirection();
                transform.DORotate(_defaultEul + new Vector3(_rotateValue * directionZ, 0, _rotateValue * directionX), _rotateDuration);
            }
            else
            {
                //移动中
                transform.DOMove(_player.position + _offset, 0.4f);
            }
            _lastPos = transform.position;
        }

        //返回值为1或-1
        private int GetXDirection()
        {
            if (transform.position.x == _lastPos.x)
                return 0;
            return transform.position.x > _lastPos.x ? -1 : 1;
        }

        //返回值为1或-1
        private int GetZDirection()
        {
            if (transform.position.z == _lastPos.z)
                return 0;
            return transform.position.z > _lastPos.z ? 1 : -1;
        }
    }
}
