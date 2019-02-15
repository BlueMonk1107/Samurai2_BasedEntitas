using System;
using System.Collections.Generic;
using Const;
using Manager;
using UnityEngine;

namespace Game.View
{
    public class UIController : MonoBehaviour
    {
        private string _namespace = "Game.View.";
        private string _postfix = "View";
        public void Init()
        {
            LoadView();
        }

        private void LoadView()
        {
            GameObject temp = null;
            Component tempComponent = null;
            foreach (GameUIName uiName in Enum.GetValues(typeof(GameUIName)))
            {
                temp = LoadManager.Single.LoadAndInstaniate(Const.Path.GAME_UI_PATH + uiName, transform);
                tempComponent = temp.AddComponent(Type.GetType(_namespace + uiName + _postfix));
                if (tempComponent is IView)
                {
                    (tempComponent as IView).Init(Contexts.sharedInstance, Contexts.sharedInstance.game.CreateEntity());
                }
            }
        }
    }
}
