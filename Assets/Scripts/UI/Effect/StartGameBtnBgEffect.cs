using Const;
using DG.Tweening;
using UnityEngine;

namespace UIFrame
{
    public class StartGameBtnBgEffect : UIEffectBase     
    {
        public override void Enter()
        {
            base.Enter();
            transform.DOScaleY(1, 0.5f);
        }

        public override void Exit()
        {
            transform.DOScaleY(0, 0.5f);
        }

        public override UiEffect GetEffectLevel()
        {
            return UiEffect.OTHERS_EFFECT;
        }
    }
}
