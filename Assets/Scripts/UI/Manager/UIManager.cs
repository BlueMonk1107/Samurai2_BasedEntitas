using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Const;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class UIManager : MonoBehaviour
    {
        private readonly Dictionary<UiId, GameObject> _prefabDictionary =  new Dictionary<UiId, GameObject>();
        private readonly Stack<UIBase> _uiStack = new Stack<UIBase>();
        private Func<UILayer,Transform> GetLayerObject;
        private Action<Transform> InitCallBack;

        public Tuple<Transform, Transform> Show(UiId id)
        {
            GameObject ui = GetPrefabObject(id);
            if (ui == null)
            {
                Debug.LogError("can not find prefab "+ id);
                return null;
            }

            UIBase uiScript = GetUiScript(ui, id);
            if(uiScript == null) 
                return null;

            InitUi(uiScript);

            Transform hideUI = null;
            if (uiScript.GetUiLayer() == UILayer.BASIC_UI)
            {
                uiScript.UiState = UIState.SHOW;
                hideUI = Hide();
            }
            else
            {
                uiScript.UiState = UIState.SHOW;
            }

            _uiStack.Push(uiScript);

            return new Tuple<Transform,Transform>(ui.transform, hideUI); ;
        }

        private Transform Hide()
        {
            if (_uiStack.Count != 0)
            {
                _uiStack.Peek().UiState = UIState.HIDE;
                return _uiStack.Peek().transform;
            }
            return null;
        }

        public Tuple<Transform, Transform> Back()
        {
            if (_uiStack.Count > 1)
            {
                UIBase hideUI = _uiStack.Pop();
                Transform shouUI = null;
                if (hideUI.GetUiLayer() == UILayer.BASIC_UI)
                {
                    hideUI.UiState = UIState.HIDE;
                    _uiStack.Peek().UiState = UIState.SHOW;
                    shouUI = _uiStack.Peek().transform;
                }
                else
                {
                    hideUI.UiState = UIState.HIDE;
                }

                return new Tuple<Transform, Transform>(shouUI, hideUI.transform);
            }
            else
            {
                Debug.LogError("uistack has one or no element");
                return null;
            }
            
        }

        public Transform GetCurrentUiTrans()
        {
            return _uiStack.Peek().transform;
        }

        public List<Transform> GetBtnParents(Transform showUI)
        {
            if (showUI != null)
            {
                return showUI.GetComponent<UIBase>().GetBtnParents();
            }
            else
            {
                return null;
            }
        }

        public void AddGetLayerObjectListener(Func<UILayer, Transform> fun)
        {
            if (fun == null)
            {
                Debug.LogError("GetLayerObject function can not be null");
                return;
            }
            GetLayerObject = fun;
        }

        public void AddInitCallBackListener(Action<Transform> callBack)
        {
            if (callBack == null)
            {
                Debug.LogError("InitCallBack function can not be null");
                return;
            }
            InitCallBack = callBack;
        }

        private void InitUi(UIBase uiScript)
        {
            if (uiScript.UiState == UIState.NORMAL)
            {
                Transform ui = uiScript.transform;
                ui.SetParent(GetLayerObject?.Invoke(uiScript.GetUiLayer()));
                ui.localPosition = Vector3.zero;
                ui.localScale = Vector3.one;
                ui.RectTransform().offsetMax = Vector2.zero;
                ui.RectTransform().offsetMin = Vector2.zero;

                InitCallBack?.Invoke(ui);
            }
        }

        private GameObject GetPrefabObject(UiId id)
        {
            if (!_prefabDictionary.ContainsKey(id) || _prefabDictionary[id] == null)
            {
                GameObject prefab = LoadManager.Instacne.Load<GameObject>(Path.UIPath, id.ToString());
                if (prefab != null)
                {
                    _prefabDictionary[id] = Instantiate(prefab);
                }
                else
                {
                    Debug.LogError("can not find prefab name:"+ id);
                }
               
            }

            return _prefabDictionary[id];
        }

        private UIBase GetUiScript(GameObject prefab, UiId id)
        {
            UIBase ui = prefab.GetComponent<UIBase>();

            if (ui == null)
            {
                return AddUiScript(prefab, id);
            }
            else
            {
                return ui;
            }
        }

        private UIBase AddUiScript(GameObject prefab, UiId id)
        {
            string scriptName = ConstValue.UI_NAMESPACE_NAME +"."+id + ConstValue.UI_SCRIPT_POSTFIX;
            Type ui = Type.GetType(scriptName);
            if (ui == null)
            {
                Debug.LogError("can not find script:"+ scriptName);
                return null;
            }
            return prefab.AddComponent(ui) as UIBase;
        }
    }
}
