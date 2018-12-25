using Const;
using DG.Tweening;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class MainViewButtonEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            transform.RectTransform().DOKill();
            transform.RectTransform().DOAnchorPos(Vector2.down * 324, 1);
        }

        public override void Exit()
        {
            transform.RectTransform().DOKill();
            transform.RectTransform().DOAnchorPos(_defaultAncherPos, 1);
        }

        public override UiEffect GetEffectLevel()
        {
            return UiEffect.VIEW_EFFECT;
        }
    }
}
