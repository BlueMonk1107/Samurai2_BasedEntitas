using UnityEngine;

namespace UIFrame
{
    public abstract class OverlayUI : UIBase
    {
        protected override void Init()
        {
            Layer = Const.UILayer.OVERLAY_UI;
        }
    }
}
