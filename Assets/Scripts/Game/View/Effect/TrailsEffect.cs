using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class TrailsEffect : MonoBehaviour
    {
        private float _duration;
        private Material _material;
        private string _colorName;

        public void Init()
        {
            _duration = 0.3f;
            _colorName = "_TintColor";
            _material = transform.GetComponent<MeshRenderer>().material;
        }

        public void Show()
        {
            Effect(1);
        }

        public void Hide()
        {
            Effect(0);
        }

        public void HideNow()
        {
            var color = _material.GetColor(_colorName);
            color.a = 0;
            _material.SetColor("_TintColor", color);
        }

        private void Effect(float endValue)
        {
            _material.DOKill();
            _material.DOFade(endValue, "_TintColor", _duration);
        }
    }
}
