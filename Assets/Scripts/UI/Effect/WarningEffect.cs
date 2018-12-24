using Const;
using DG.Tweening;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class WarningEffect : UIEffectBase     
    {
        public override void Enter()
        {
            base.Enter();
            Init();
            transform.RectTransform().DOAnchorPosX(0, 1);
        }

        private void Init()
        {
            _defaultAncherPos = new Vector2(1547f, 0);
            transform.RectTransform().anchoredPosition = _defaultAncherPos;
        }

        public override void Exit()
        {
            transform.RectTransform().DOAnchorPosX(_defaultAncherPos.x, 1);
        }

        public override UiEffect GetEffectLevel()
        {
           return UiEffect.VIEW_EFFECT;
        }
    }
}
