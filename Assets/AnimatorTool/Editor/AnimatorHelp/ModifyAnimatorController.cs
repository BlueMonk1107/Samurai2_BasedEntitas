using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace CustomTool
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AnimatorHelp), true)]
    public class ModifyAnimatorController : Editor
    {
        private AnimatorHelp _help;

        private void OnEnable()
        {
            InitData();
            _help.InitTransitionsDic();
        }

        private void InitData()
        {
            try
            {
                _help = target as AnimatorHelp;
                if (string.IsNullOrEmpty(_help.name))
                {
                    Debug.Log("Help脚本名称为空");
                }
                else
                {
                    string[] data = _help.name.Split('#');
                    string aniName = data[0];
                    int nameHash = int.Parse(data[1]);

                    _help.Controller = AnimatorToolWindow.HelpControllers.FirstOrDefault(u => u != null && u.name == aniName);
                    if (_help.Controller == null)
                    {
                        Debug.LogError("未找到对应状态机 名称为：" + aniName);
                    }
                    else
                    {
                        var states = _help.Controller.GetAllAnimatorStates();
                        _help.AnimatorState = states.FirstOrDefault(u => u.nameHash == nameHash);
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
            AddFun(true,"请选择要修改的过渡状态", () =>
            {
                SelectAllTrasion();
                TrasionToggle();
            });

            AddFun(GetTransitionState(), "修改参数", () =>
            {
                base.DrawDefaultInspector();

                if (GUILayout.Button("修改"))
                {
                    if (EditorUtility.DisplayDialog("确认修改", "是否确认修改当前选中过渡状态数据", "是", "否"))
                    {
                        ChangeTransitionData();
                    }
                }
            });

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_help);
            }
        }


        private void ChangeTransitionData()
        {
            foreach (KeyValuePair<AnimatorStateTransition, bool> pair in _help.TransitionsDic)
            {
                if (pair.Value)
                {
                    foreach (ParaEnum value in Enum.GetValues(typeof(ParaEnum)))
                    {
                        if (_help.GetSelectedData(value))
                        {
                            var to = pair.Key.GetType().GetProperty(value.ToString());
                            var from = _help.TransitionPara.GetType().GetField(value.ToString());
                            to.SetValue(pair.Key, from.GetValue(_help.TransitionPara));
                        }
                    }

                }
            }

            EditorUtility.DisplayDialog("", "修改完成", "是");
        }

        private void AddFun(bool isShow,string title, Action action)
        {
            if (!isShow)
                return;
            EditorGUILayout.BeginVertical(GUI.skin.box);

            GUILayout.SelectionGrid(1, new[] { title }, 1);
            action?.Invoke();

            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
        }

        private void SelectAllTrasion()
        {
            bool lastToggleState = _help.SelectAllTransion;
            _help.SelectAllTransion = GUILayout.Toggle(_help.SelectAllTransion, "全选");

            if (_help.SelectAllTransion)
            {
                SetTransitionState(true);
            }
            else
            {
                if (_help.SelectAllTransion != lastToggleState)
                {
                    SetTransitionState(false);
                }
            }
        }

        private void SetTransitionState(bool isSelect)
        {
            foreach (var transition in _help.TransitionsList)
            {
                _help.TransitionsDic[transition] = isSelect;
            }
        }

        /// <summary>
        /// 是否有选中的过渡状态
        /// </summary>
        private bool GetTransitionState()
        {
            foreach (var transition in _help.TransitionsList)
            {
                if (_help.TransitionsDic[transition])
                {
                    return true;
                }
            }

            return false;
        }

        private void TrasionToggle()
        {
            foreach (var transition in _help.TransitionsList)
            {
                if (transition.destinationState != null)
                {
                    _help.TransitionsDic[transition] = GUILayout.Toggle(_help.TransitionsDic[transition], "To  " + transition.destinationState.name);
                }

                if (!_help.TransitionsDic[transition])
                {
                    _help.SelectAllTransion = false;
                }
            }
        }
    }
}
