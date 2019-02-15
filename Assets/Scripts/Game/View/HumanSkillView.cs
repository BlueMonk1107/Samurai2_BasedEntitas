using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Game.Effect;
using Game.Service;
using Manager;
using Module;
using Module.Timer;
using UnityEngine;

namespace Game.View
{
    public class HumanSkillView : ViewBase, IGameValidHumanSkillListener
    {
        private List<HumanSkillItem> _itemList;
        private SkillCodeModule _codeModule;
        private float _effectDuration;
        private ITimerService _timerService;
        private string _timerID;
        private ITimer _timer;

        public override void Init(Contexts contexts, IEntity entity)
        {
            base.Init(contexts, entity);
            
            _entity.AddGameValidHumanSkillListener(this);
            _itemList = new List<HumanSkillItem>();
            _codeModule =  new SkillCodeModule();
            _effectDuration = 0.5f;
            _timerID = "HumanSkillView";
            HideImage();
        }

        public void OnGameValidHumanSkill(GameEntity entity, int SkillCode)
        {
            var codeString = _codeModule.GetCodeString(SkillCode);
            ShowItem(codeString);
            gameObject.ShowAllImageEffect(_effectDuration);

            StartTimer();
        }

        private void InitTimerService()
        {
            if (_timerService == null)
            {
                _timerService = Contexts.sharedInstance.service.gameServiceTimerService.TimerService;
            }
        }

        private void StartTimer()
        {
            InitTimerService();

            _timer = _timerService.CreatOrRestartTimer(_timerID, 1, false);
            _timer.AddCompleteListener(HideImage);
        }

        private void HideImage()
        {
            gameObject.HideAllImageEffect(_effectDuration);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// 生成子项 传入参数为“xo”的技能编码字符串
        /// </summary>
        /// <param name="codeString"></param>
        public void ShowItem(string codeString)
        {
            SpwanItem(codeString);
            ShowCode(codeString);
        }

        private void SpwanItem(string codeString)
        {
            if (_itemList.Count < codeString.Length)
            {
                foreach (char c in codeString)
                {
                    SpwanNewItem();
                }
            }
        }

        private void SpwanNewItem()
        {
            var go = LoadManager.Single.LoadAndInstaniate(Const.Path.HUMAN_SKILL_ITEM_UI_PATH, transform);
            var item = go.AddComponent<HumanSkillItem>();
            item.Init();
            _itemList.Add(item);
        }

        private void ShowCode(string codeString)
        {
            for (int i = 0; i < _itemList.Count; i++)
            {
                if (i < codeString.Length)
                {
                    _itemList[i].ChangeSprite(codeString[i]);
                }
                else
                {
                    _itemList[i].SetActive(false);
                }
            }
        }


    }
}
