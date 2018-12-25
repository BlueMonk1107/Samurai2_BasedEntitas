using Const;
using UnityEngine;
using DG.Tweening;
using Util;

namespace UIFrame
{
    public class GameNameEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            float time = 1;
            transform.DOKill();
            transform.RectTransform().DOKill();
            transform.DOScale(Vector3.one * 2, time);
            transform.RectTransform().DOAnchorPos(_defaultAncherPos, time).OnComplete(() => _onExitComplete?.Invoke());
            RootManager.Instance.PlayAudio(UIAudioName.UI_logo_in);
        }

        public override void Exit()
        {
            float time = 1;
            transform.DOKill();
            transform.RectTransform().DOKill();
            transform.DOScale(Vector3.one * 1.5f, time);
            transform.RectTransform().DOAnchorPos(new Vector2(497f, 198f), time).OnComplete(() => _onEnterComplete?.Invoke());
            RootManager.Instance.PlayAudio(UIAudioName.UI_logo_out);
        }

        public override UiEffect GetEffectLevel()
        {
            return UiEffect.VIEW_EFFECT;
        }
    }
}
