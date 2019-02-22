using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class TrailsEffect : MonoBehaviour
    {
        private Material _material;
        private float _clipLength;
        private Sequence _sequence;
        string _colorName = "_TintColor";
        private Animation _dustAni;

        public void Init(float clipLength)
        {
            _clipLength = clipLength;

            _material = transform.GetComponent<MeshRenderer>().material;

            InitDust();
        }

        private void InitDust()
        {
            _dustAni = transform.GetComponentInChildren<Animation>();
        }

        public void Show(int code)
        {
            float duration = 0.2f;
            float intervalTime = 0.2f;
            float showTime = _clipLength - intervalTime - duration * 2 - 0.2f;

            Light(intervalTime, duration, showTime);

            ShowDust();

            if (code.ToString().Length >= 3)
            {
                Shake(intervalTime + showTime*0.5f);
            }
        }

        private void Light(float intervalTime,float duration,float showTime)
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(intervalTime);
            _sequence.Append(_material.DOFade(1, _colorName, duration));
            _sequence.AppendInterval(showTime);
            _sequence.Append(_material.DOFade(0, _colorName, duration));
        }

        private async void Shake(float delayTime)
        {
            await Task.Delay(TimeSpan.FromSeconds(delayTime));
            Contexts.sharedInstance.game.ReplaceGameCameraState(CameraAniName.SHAKE_ANI);
        }

        public void HideNow()
        {
            var color = _material.GetColor(_colorName);
            color.a = 0;
            _material.SetColor(_colorName, color);
            SetDustActive(false);
        }

        private void ShowDust()
        {
           if(_dustAni == null)
                return;

            SetDustActive(true);
            _dustAni.Play();

            StopAllCoroutines();
            StartCoroutine(WiatDustEnd());
        }

        private IEnumerator WiatDustEnd()
        {
            float length = _dustAni.clip.length;
            yield return new WaitForSeconds(length);
            SetDustActive(false);
        }

        private void SetDustActive(bool isActive)
        {
            if (_dustAni == null)
                return;
            _dustAni.gameObject.SetActive(isActive);
        }
    }
}
