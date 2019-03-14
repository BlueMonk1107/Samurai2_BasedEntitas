using System;
using Const;
using UnityEngine;

namespace Game
{
    public class StartPartTrigger : MonoBehaviour
    {
        private Action _startCallBack;

        public void Init(Action startCallBack)
        {
            _startCallBack = startCallBack;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagAndLayer.PLAYER_TAG && _startCallBack != null)
            {
                _startCallBack.Invoke();
                _startCallBack = null;
            }
        }
    }
}
