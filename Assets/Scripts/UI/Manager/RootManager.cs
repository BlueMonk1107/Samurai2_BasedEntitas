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
        private UIAudioManager _audioManager;

        private void Awake()
        {
            Instance = this;
            _uiManager = gameObject.AddComponent<UIManager>();
            _effectManager = gameObject.AddComponent<UIEffectManager>();
            _layerManager = gameObject.AddComponent<UILayerManager>();
            _inputManager = gameObject.AddComponent<InputManager>();
            _btnStateManager = gameObject.AddComponent<BtnStateManager>();
            _audioManager = gameObject.AddComponent<UIAudioManager>();

            _audioManager.Init(Path.UI_AUDIO_PATH,LoadManager.Instacne.LoadAll<AudioClip>);
            _uiManager.AddGetLayerObjectListener(_layerManager.GetLayerObject);
            _uiManager.AddInitCallBackListener((uiTrans) =>
            {
                var list = _uiManager.GetBtnParents(uiTrans);
                _btnStateManager.InitBtnParent(list);
            });

            _audioManager.PlayBg(UIAudioName.UI_bg.ToString());
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
            ShowUI(uiPara.Item1);
            HideUI(uiPara.Item2);
        }

        private void ShowUI(Transform showUI)
        {
            ShowEffect(showUI);
            ShowUIAudio();
        }

        private void HideUI(Transform hideUI)
        {
            HideEffect(hideUI);
            HideUIAudio();
        }

        private void ShowUIAudio()
        {
            _audioManager.Play(UIAudioName.UI_in.ToString());
        }

        private void HideUIAudio()
        {
            _audioManager.Play(UIAudioName.UI_out.ToString());
        }

        private void ShowEffect(Transform showUI)
        {
            if (showUI == null)
            {
                _effectManager.ShowOthersEffect(_uiManager.GetCurrentUiTrans());
            }
            else
            {
                _effectManager.Show(showUI);
            }
        }

        private void HideEffect(Transform hideUI)
        {
            if (hideUI == null)
            {
                _effectManager.HideOthersEffect(_uiManager.GetBaiscUiTrans());
            }
            else
            {
                _effectManager.Hide(hideUI);
            }
        }

        public void PlayAudio(UIAudioName name)
        {
            _audioManager.Play(name.ToString());
        }

        private void ShowBtnState(Transform ui)
        {
            _btnStateManager.Show(ui);
        }
    }
}
