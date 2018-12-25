using System.Collections.Generic;
using Const;
using Manager;
using UnityEngine;

namespace UIFrame
{
    public class LoadingView : OverlayUI     
    {
       
        public override UiId GetUiId()
        {
            return UiId.Loading;
        }

        public override List<Transform> GetBtnParents()
        {
            return null;
        }

        protected override void Show()
        {
            base.Show();
            StartCoroutine(LoadSceneManager.Single.LoadSceneAsync(DataManager.Single.GetSceneName()));
            LoadSceneManager.Single.AllowSwitchScene();
            transform.SetAsLastSibling();
        }
    }
}
