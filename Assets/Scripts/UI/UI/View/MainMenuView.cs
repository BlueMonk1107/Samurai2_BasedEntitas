using System.Collections.Generic;
using Const;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UIFrame
{
    public class MainMenuView : BasicUI
    {
        public override UiId GetUiId()
        {
            return UiId.MainMenu;
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
            transform.AddBtnListener("StartGame", () => { RootManager.Instance.Show(UiId.StartGame); });
            transform.AddBtnListener("DOJO", () => { });
            transform.AddBtnListener("Help", () => { });
            transform.AddBtnListener("ExitGame", () => { Application.Quit(); });
        }
    }
}
