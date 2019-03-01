using UnityEditor;
using UnityEngine;

namespace CustomTool
{
    [CustomPropertyDrawer(typeof(SubAnimatorMachineItem))]
    public class SubAnimatorMachineItemShower : PropertyDrawer     
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position,label, property))
            {
                GUI.Label(position, label);

                var name = property.FindPropertyRelative("SubMachineName");
                var anis = property.FindPropertyRelative("AnimationObjects");

                var nameRect = new Rect(position)
                {
                    y = position.y + EditorGUIUtility.singleLineHeight,
                    height = EditorGUIUtility.singleLineHeight
                };

                var aniRect = new Rect(nameRect)
                {
                    y = nameRect.y + EditorGUIUtility.singleLineHeight
                };

                EditorGUI.PropertyField(nameRect, name, new GUIContent("子状态机名称"));
                EditorGUI.PropertyField(aniRect, anis, new GUIContent("动画片段父物体数组"),true);
            }
        }
    }
}
