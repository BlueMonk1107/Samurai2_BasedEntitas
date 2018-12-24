using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class RootManager: MonoBehaviour
    {
        public static RootManager Instance { get; private set; } 

        private UIManager _uiManager;
        private UIEffectManager _effectManager;
        private UILayerManager _layerManager;
        private InputManager _inputManager;
        private BtnStateManager _btnStateManager;

        private void Awake()
        {
            Instance = this;
            _uiManager = gameObject.AddComponent<UIManager>();
            _effectManager = gameObject.AddComponent<UIEffectManager>();
            _layerManager = gameObject.AddComponent<UILayerManager>();
            _inputManager = gameObject.AddComponent<InputManager>();
            _btnStateManager = gameObject.AddComponent<BtnStateManager>();

            _uiManager.AddGetLayerObjectListener(_layerManager.GetLayerObject);
            _uiManager.AddInitCallBackListener((uiTrans) =>
            {
                var list = _uiManager.GetBtnParents(uiTrans);
                _btnStateManager.InitBtnParent(list);
            });
        }

        private void Start()
        {
            Show(UiId.MainMenu);
        }

        public void Show(UiId id)
        {
            var uiPara = _uiManager.Show(id);
            ExcuteEffect(uiPara);
            ShowBtnState(uiPara.Item1);
        }

        public void Back()
        {
            var uiPara = _uiManager.Back();
            ExcuteEffect(uiPara);
            ShowBtnState(_uiManager.GetCurrentUiTrans());
        }

        public void ButtonLeft()
        {
            _btnStateManager.Left();
        }

        public void ButtonRight()
        {
            _btnStateManager.Right();
        }

        public void SelectedButton()
        {
            _btnStateManager.SelectedButton();
        }

        private void ExcuteEffect(Tuple<Transform, Transform> uiPara)
        {
            _effectManager.Show(uiPara.Item1);
            _effectManager.Hide(uiPara.Item2);
        }

        private void ShowBtnState(Transform ui)
        {
            _btnStateManager.Show(ui);
        }
    }
}
