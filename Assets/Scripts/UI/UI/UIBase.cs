using System;
using System.Collections.Generic;
using Const;
using UnityEngine;

namespace UIFrame
{
    //ui基础类
    public abstract class UIBase : MonoBehaviour
    {
        //当前UI的层级
        public UILayer Layer { get; protected set; }
        private UIState _uiState = UIState.NORMAL;

        public UIState UiState
        {
            get { return _uiState; }
            set { HandleUiState(value); }
        }

        private void HandleUiState(UIState value)
        {
            switch (value)
            {
                case UIState.INIT:
                    if (_uiState == UIState.NORMAL)
                    {
                        Init();
                    }
                    break;
                case UIState.SHOW:
                    if (_uiState == UIState.NORMAL)
                    {
                        Init();
                        Show();
                    }
                    else
                    {
                        Show();
                    }
                    break;
                case UIState.HIDE:
                    Hide();
                    break;
            }
        }

        protected virtual void Init()
        {
        }

        protected virtual void Show()
        {
            
        }

        protected virtual void Hide()
        {

        }

        protected virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public abstract UiId GetUiId();

        public abstract List<Transform> GetBtnParents();
    }
}
