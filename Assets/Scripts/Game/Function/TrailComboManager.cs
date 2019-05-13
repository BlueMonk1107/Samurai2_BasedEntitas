using System.Collections.Generic;
using Const;
using Entitas;
using Module;
using Module.Skill;
using UnityEngine;

namespace Game.View
{
    public class TrailComboManager : ViewBase, IGameStartHumanSkillListener
    {
        private string prefixName = "trail_";
        private Dictionary<int, TrailsEffect> _trailsDic;
        private Dictionary<int, float> _clipLengthDic;
        private SkillCodeModule _module;

        public override void Init(Contexts contexts, IEntity entity)
        {
            base.Init(contexts, entity);
            _entity.AddGameStartHumanSkillListener(this);

            _trailsDic = new Dictionary<int, TrailsEffect>();
            _clipLengthDic = new Dictionary<int, float>();
            _module = new SkillCodeModule();
        }

        public void Init(Contexts contexts, IEntity entity, Animator animator)
        {
            Init(contexts, entity);

            InitClipLengthDic(animator);
            InitTrailsDic();
            HideAllTrails();
        }

        private void InitTrailsDic()
        {
            int code = 0;
            float length = 0;
            foreach (Transform tran in transform)
            {
                code = GetSkillCode(tran.name);
                if (_clipLengthDic.ContainsKey(code))
                {
                    length = _clipLengthDic[code];
                    _trailsDic[code] = tran.gameObject.AddComponent<TrailsEffect>();
                    _trailsDic[code].Init(length);
                }
                else
                {
                    Debug.LogWarning("动画中未找到对应Code为"+code+"的动画片段");
                    _trailsDic[code] = tran.gameObject.AddComponent<TrailsEffect>();
                    _trailsDic[code].Init(0);
                }
            }
        }

        private void InitClipLengthDic(Animator animator)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            int code = 0;

            foreach (AnimationClip clip in clips)
            {
                code = _module.GetSkillCode(clip.name, ConstValue.SKILL_ANI_PREFIX, "");
                if (code < 0)
                    continue;
                _clipLengthDic[code] = clip.length;
            }
        }

        private int GetSkillCode(string codeName)
        {
            return _module.GetSkillCode(codeName, prefixName, "");
        }

        private void ShowTrails(int code)
        {
            _trailsDic[code].Show(code);
        }


        private void HideAllTrails()
        {
            foreach (KeyValuePair<int, TrailsEffect> pair in _trailsDic)
            {
                pair.Value.HideNow();
            }
        }

        public void OnGameStartHumanSkill(GameEntity entity, int SkillCode)
        {
            ShowTrails(SkillCode);
        }
    }
}
