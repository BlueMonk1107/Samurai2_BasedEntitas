using Module;
using Module.Skill;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class HumanSkillItem : MonoBehaviour
    {
        private HumanSkillSprite _humanSkill;
        private Image _image;

        public void Init()
        {
            _humanSkill = GetComponent<HumanSkillSprite>();
            _image = GetComponent<Image>();
        }

        public void ChangeSprite(char code)
        {
            if (code.ToString() == SkillButton.O.ToString())
            {
                _image.sprite = _humanSkill.O;
                SetActive(true);
            }
            else if (code.ToString() == SkillButton.X.ToString())
            {
                _image.sprite = _humanSkill.X;
                SetActive(true);
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
