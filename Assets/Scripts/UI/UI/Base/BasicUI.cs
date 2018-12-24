using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class BasicUI : UIBase
    {
        public override UILayer GetUiLayer()
        {
            return UILayer.BASIC_UI;
        }
    }
}
