using System;
using Const;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    /// <summary>
    /// 拓展方法工具类
    /// </summary>
    public static class ExtendUtil     
    {
        public static void AddBtnListener(this RectTransform rect, Action action)
        {
            var button = rect.GetComponent<Button>() ?? rect.gameObject.AddComponent<Button>();

            button.onClick.AddListener(()=> action());
        }

        public static void AddBtnListener(this Transform rect, Action action)
        {
            var button = rect.GetComponent<Button>() ?? rect.gameObject.AddComponent<Button>();

            button.onClick.AddListener(() => action());
        }

        public static RectTransform RectTransform(this Transform transform)
        {
            var rect = transform.GetComponent<RectTransform>();

            if (rect != null)
            {
                return rect;
            }
            else
            {
                Debug.LogError("can not find RectTransform");
                return null;
            }
        }

        public static Image Image(this Transform transform)
        {
            var image = transform.GetComponent<Image>();

            if (image != null)
            {
                return image;
            }
            else
            {
                Debug.LogError("can not find Image");
                return null;
            }
        }

        public static Button Button(this Transform transform)
        {
            var button = transform.GetComponent<Button>();

            if (button != null)
            {
                return button;
            }
            else
            {
                Debug.LogError("can not find Image");
                return null;
            }
        }

        public static Transform GetBtnParent(this Transform transform)
        {
            var parent = transform.Find(ConstValue.BUTTON_PARENT_NAME);
            if (parent == null)
            {
              Debug.LogError("can not find ButtonParent name:"+ ConstValue.BUTTON_PARENT_NAME);
                return null;
            }
            else
            {
                return parent;
            }
           
        }

        public static void AddBtnListener(this Transform transform, string btnName, Action callBack)
        {
            var buttonTrans = transform.Find(ConstValue.BUTTON_PARENT_NAME + "/" + btnName);
            if (buttonTrans == null)
            {
                Debug.LogError("can not find button, name:"+ btnName);
            }
            else
            {
                buttonTrans.AddBtnListener(callBack);
            }
        }
    }
}
