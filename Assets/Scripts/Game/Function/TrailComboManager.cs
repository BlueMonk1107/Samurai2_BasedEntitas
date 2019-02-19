using System.Collections.Generic;
using Entitas;
using Module;
using UnityEngine;

namespace Game.View
{
    public class TrailComboManager : ViewBase,IGameStartHumanSkillListener,IGameEndHumanSkillListener
    {
        private string prefixName = "trail_";
        private Dictionary<int, TrailsEffect> _trailsDic;

        public override void Init(Contexts contexts, IEntity entity)
        {
            base.Init(contexts, entity);
            _entity.AddGameStartHumanSkillListener(this);
            _entity.AddGameEndHumanSkillListener(this);

            _trailsDic = new Dictionary<int, TrailsEffect>();
            InitTrailsDic();
            HideAllTrails();
        }

        private void InitTrailsDic()
        {
            foreach (Transform tran in transform)
            {
                _trailsDic[GetSkillCode(tran.name)] = tran.gameObject.AddComponent<TrailsEffect>();
                _trailsDic[GetSkillCode(tran.name)].Init();
            }
        }

        private int GetSkillCode(string codeName)
        {
            SkillCodeModule module = new SkillCodeModule();
            return module.GetSkillCode(codeName, prefixName, "");
        }

        private void ShowTrails(int code)
        {
            SetShowOrHide(_trailsDic[code], true);
        }

        private void HideTrails(int code)
        {
            SetShowOrHide(_trailsDic[code], false);
        }

        private void HideAllTrails()
        {
            foreach (KeyValuePair<int, TrailsEffect> pair in _trailsDic)
            {
                pair.Value.HideNow();
            }
        }

        private void SetShowOrHide(TrailsEffect effect, bool isActive)
        {
            if (isActive)
            {
                effect.Show();
            }
            else
            {
                effect.Hide();
            }
        }

        public void OnGameStartHumanSkill(GameEntity entity, int SkillCode)
        {
            ShowTrails(SkillCode);
        }

        public void OnGameEndHumanSkill(GameEntity entity, int SkillCode)
        {
            HideTrails(SkillCode);
        }
    }
}
