using DG.Tweening;
using UnityEngine;

namespace Game.GamePart
{
    public class ZamekEffect : MonoBehaviour
    {
        private Vector3 _defaultScale;
        private float _duration;

        public void Init()
        {
            _defaultScale = transform.localScale;
            _duration = 1.5f;
        }

        public void SetOpenState(bool isOpen)
        {
            if (isOpen)
            {
                HideZamek();
            }
            else
            {
                ShowZamek();
            }
        }

        private void ShowZamek()
        {
            transform.DOKill();
            transform.localScale = Vector3.zero;

            transform.DOScale(_defaultScale, _duration).SetEase(Ease.OutElastic);
        }

        private void HideZamek()
        {
            transform.DOKill();
            transform.localScale = _defaultScale;

            transform.DOScale(Vector3.zero, _duration).SetEase(Ease.OutElastic);
        }
    }
}
