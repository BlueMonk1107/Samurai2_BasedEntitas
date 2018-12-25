using System.Collections.Generic;
using Const;
using Data;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UIFrame
{
    public class StartGameView : BasicUI     
    {
        public override UiId GetUiId()
        {
            return UiId.StartGame;
        }

        public override List<Transform> GetBtnParents()
        {
            List<Transform> list = new List<Transform>();
            list.Add(transform.GetBtnParent());
            return list;
        }

        protected override void Init()
        {
            base.Init();
            transform.AddBtnListener("Continue",() => { });
            transform.AddBtnListener("Easy", LoadScene);
            transform.AddBtnListener("Normal", LoadScene);
            transform.AddBtnListener("Hard", LoadScene);
        }

        protected override void Show()
        {
            base.Show();
            SetContinueBtnState();
        }

        private void SetContinueBtnState()
        {
            bool exist = DataManager.JudgeExistData();
            transform.GetBtnParent().Find("Continue").gameObject.SetActive(exist);
        }

        private void LoadScene()
        {
            bool exist = DataManager.JudgeExistData();
            if (exist)
            {
                RootManager.Instance.Show(UiId.NewGameWarning);
            }
            else
            {
                //Ìø×ª³¡¾°
            }
        }
    }
}
