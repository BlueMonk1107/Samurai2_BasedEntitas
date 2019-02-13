using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Manager;
using UnityEngine;

namespace Game.View
{
    public class HumanSkillView : ViewBase
    {
        private List<HumanSkillItem> _itemList; 

        public override void Init(Contexts contexts,IEntity entity)         
        {
             base.Init(contexts, entity);
            _itemList = new List<HumanSkillItem>();
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
                if (_itemList.Count <= codeString.Length)
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
