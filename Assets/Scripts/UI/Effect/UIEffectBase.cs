using System;
using Const;
using UnityEngine;
using Util;

namespace UIFrame
{
    public abstract class UIEffectBase : MonoBehaviour
    {
        protected Vector2 _defaultAncherPos = Vector2.zero;

        protected Action _onEnterComplete;
        protected Action _onExitComplete;

        public virtual void Enter()
        {
            if (_defaultAncherPos == Vector2.zero)
            {
                _defaultAncherPos = transform.RectTransform().anchoredPosition;
            }
        }
        public abstract void Exit();

        public virtual void OnEnterComplete(Action enterAction)
        {
            _onEnterComplete = enterAction;
        }

        public virtual void OnExitComplete(Action exitAction)
        {
            _onExitComplete = exitAction;
        }

        public abstract UiEffect GetEffectLevel();
    }
}
