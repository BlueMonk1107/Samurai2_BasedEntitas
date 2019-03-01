using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Util;

namespace CustomTool
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AnimatorHelp), true)]
    public class ModifyAnimatorController : Editor
    {
        private AnimatorController _controller;
        private AnimatorState _animatorState;

        private void OnEnable()
        {
            try
            {
                var help = target as AnimatorHelp;
                if (string.IsNullOrEmpty(help.name))
                {
                    Debug.Log("Help脚本名称为空");
                }
                else
                {
                    string[] data = help.name.Split('#');
                    string aniName = data[0];
                    int nameHash = int.Parse(data[1]);

                    _controller = AnimatorToolWindow.HelpControllers.FirstOrDefault(u => u != null && u.name == aniName);
                    if (_controller == null)
                    {
                        Debug.LogError("未找到对应状态机 名称为："+ aniName);
                    }
                    else
                    {
                        var states = _controller.GetAllAnimatorStates();
                        _animatorState = states.FirstOrDefault(u => u.nameHash == nameHash);
                    }
                }

            }
            catch (Exception)
            {
                Debug.LogError("类型转换出错");
                throw;
            }
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();



            GUILayout.Toggle(true, "ssss");
        }
    }
}
