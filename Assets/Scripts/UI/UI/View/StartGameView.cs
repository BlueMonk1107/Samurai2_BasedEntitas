using System.Collections.Generic;
using Const;
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
            transform.AddBtnListener("Easy", () => { RootManager.Instance.Show(UiId.NewGameWarning);});
            transform.AddBtnListener("Normal", () => { });
            transform.AddBtnListener("Hard", () => { });
        }
    }
}
