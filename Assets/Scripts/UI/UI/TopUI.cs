using UnityEngine;

namespace UIFrame
{
    public abstract class TopUI : UIBase
    {
        protected override void Init()
        {
            Layer = Const.UILayer.TOP_UI;
        }
    }
}
