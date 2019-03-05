using System;
using UnityEditor;
using UnityEngine;

namespace CustomTool
{
    [CustomPropertyDrawer(typeof(CustomTransitionPara))]
    public class CustomTransitionParaDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent lable)
        {
            using (new EditorGUI.PropertyScope(position, lable, property))
            {
                AnimatorHelp help = property.serializedObject.targetObject as AnimatorHelp;

                if (help != null)
                {
                    foreach (ParaEnum value in Enum.GetValues(typeof(ParaEnum)))
                    {
                        if (help.GetSelectedData(value))
                        {
                            var para = property.FindPropertyRelative(value.ToString());
                            EditorGUILayout.PropertyField(para);
                        }
                    }
                }
            }
        }
    }

    [CustomPropertyDrawer(typeof(MultiSelectEnumAttribute))]
    public class MultiSelectEnumDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent lable)
        {
            using (new EditorGUI.PropertyScope(position, lable, property))
            {
                property.intValue = EditorGUILayout.MaskField("选择当前需要修改的参数", property.intValue,
                    property.enumDisplayNames);
            }
        }
    }
}
