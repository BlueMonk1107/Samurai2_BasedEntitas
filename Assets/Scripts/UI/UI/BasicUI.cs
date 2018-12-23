using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class BasicUI : UIBase
    {
        protected override void Init()
        {
            Layer = Const.UILayer.BASIC_UI;
        }
    }
}
