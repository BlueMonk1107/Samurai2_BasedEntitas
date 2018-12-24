using System.Collections.Generic;
using Const;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class NewGameWarningView : OverlayUI
    {
        public override UiId GetUiId()
        {
            return UiId.NewGameWarning;
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
            transform.AddBtnListener("Yes", () => { Debug.Log("yes");});
            transform.AddBtnListener("No", () => { RootManager.Instance.Back(); });
        }
    }
}
