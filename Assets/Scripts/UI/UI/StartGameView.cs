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
            list.Add(transform.Find("Buttons"));
            return list;
        }

        public void Start()
        {
            transform.Find("Buttons/Continue").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Easy").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Normal").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Hard").RectTransform().AddBtnListener(() => { });
        }
    }
}
