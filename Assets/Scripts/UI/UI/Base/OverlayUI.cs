using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class OverlayUI : UIBase
    {
        public override UILayer GetUiLayer()
        {
            return UILayer.OVERLAY_UI;
        }
    }
}
