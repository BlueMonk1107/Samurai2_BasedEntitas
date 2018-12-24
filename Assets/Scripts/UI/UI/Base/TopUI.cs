using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class TopUI : UIBase
    {
        public override UILayer GetUiLayer()
        {
            return UILayer.TOP_UI;
        }
    }
}
