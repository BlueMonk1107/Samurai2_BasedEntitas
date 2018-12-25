using System.Collections.Generic;
using Const;
using DG.Tweening;
using Manager;
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
            transform.AddBtnListener("Continue",() => LoadScene(true));
            transform.AddBtnListener("Easy", () =>
            {
                LoadScene(false);
                DataManager.Single.DifficultLevel = DifficultLevel.EASY;
            });
            transform.AddBtnListener("Normal", () =>
            {
                LoadScene(false);
                DataManager.Single.DifficultLevel = DifficultLevel.NORMAL;
            });
            transform.AddBtnListener("Hard", () =>
            {
                LoadScene(false);
                DataManager.Single.DifficultLevel = DifficultLevel.HARD;
            });
        }

        protected override void Show()
        {
            base.Show();
            SetContinueBtnState();
        }

        private void SetContinueBtnState()
        {
            bool exist = DataManager.Single.JudgeExistData();
            transform.GetBtnParent().Find("Continue").gameObject.SetActive(exist);
        }

        private void LoadScene(bool isContinue)
        {
            if (isContinue)
            {
                CotinueGame();
            }
            else
            {
                NewGame();
            }
        }

        private void NewGame()
        {
            bool exist = DataManager.Single.JudgeExistData();
            if (exist)
            {
                RootManager.Instance.Show(UiId.NewGameWarning);
            }
            else
            {
                RootManager.Instance.Show(UiId.Loading);
            }
        }

        private void CotinueGame()
        {
            RootManager.Instance.Show(UiId.Loading);
        }
    }
}
