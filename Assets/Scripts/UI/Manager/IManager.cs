using Const;
using UnityEngine;

namespace UIFrame
{
    public interface IUIManager
    {
        void Show(UiId id);

        void Hide();

        void Back();
    }
}
