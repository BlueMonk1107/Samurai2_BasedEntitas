using System;
using Const;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace UIFrame
{
    public class SelectedBtn : MonoBehaviour,IPointerEnterHandler
    {
        public SelectedState SelectedState
        {
            set
            {
                switch (value)
                {
                    case SelectedState.SELECTED:
                        Selected();
                        break;
                    case SelectedState.UNSELECTED:
                        CancelSelected();
                        break;
                }
            }
        }

        public int Index
        {
            get { return transform.GetSiblingIndex(); }
        }

        private Action<SelectedBtn> _selectAction;

        private Color _defaultColor;

        private void Awake()
        {
            SaveDefaultColor(transform);
        }

        public void Selected()
        {
            if (!JudgeException(transform))
            {
                PlayEffect(transform);
            }
        }

        public void AddSelectActionListener(Action<SelectedBtn> action)
        {
            _selectAction = action;
        }

        public void CancelSelected()
        {
            KillEffect(transform);
        }

        public void SelectedButton()
        {
            transform.Button().onClick.Invoke();
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            _selectAction?.Invoke(this);
        }
    }
}
