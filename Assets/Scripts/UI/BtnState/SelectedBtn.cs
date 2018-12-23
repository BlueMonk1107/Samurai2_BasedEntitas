using DG.Tweening;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class SelectedBtn : MonoBehaviour
    {
        public int Index
        {
            get { return transform.GetSiblingIndex(); }
        }
        private Color _defaultColor;
        public void Selected()
        {
            if (!JudgeException(transform))
            {
                SaveDefaultColor(transform);
                PlayEffect(transform);
            }
        }

        public void CancelSelected()
        {
            KillEffect(transform);
        }

        private void KillEffect(Transform btn)
        {
            if (btn == null)
                return;

            btn.Image().DOKill();
            btn.Image().color = _defaultColor;
        }

        private bool JudgeException(Transform btn)
        {
            return btn.Button() == null || btn.Image() == null;
        }

        private void SaveDefaultColor(Transform btn)
        {
            _defaultColor = btn.Image().color;
        }

        private void PlayEffect(Transform btn)
        {
            btn.Image().DOColor(new Color32(154, 170, 255, 255), 1).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
