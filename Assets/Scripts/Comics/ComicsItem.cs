using DG.Tweening;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class ComicsItem : MonoBehaviour
    {
        public int Page { get; private set; }

        public void Init(Sprite sprite,int page)
        {
            if (transform.Image() != null)
            {
                transform.Image().sprite = sprite;
            }
            Page = page;
        }

        public void SetParent(Transform parnet)
        {
            transform.SetParent(parnet);
        }

        public void SetParnetAndPosition(Transform parnet)
        {
            SetParent(parnet);
            transform.RectTransform().anchoredPosition = Vector2.zero;
        }

        public void Move(Transform parnet)
        {
            SetParent(parnet);
            transform.RectTransform().DOKill();
            transform.RectTransform().DOAnchorPos(Vector2.zero, 1).SetEase(Ease.Linear);
        }
    }
}
