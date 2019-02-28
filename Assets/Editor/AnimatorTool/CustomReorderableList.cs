using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CustomTool
{
    public class CustomReorderableList
    {
        private ReorderableList _reorderableList;
        private SerializedProperty _property;

        public CustomReorderableList(SerializedObject serializedObject, SerializedProperty property)
        {
            _property = property;
            _reorderableList = new ReorderableList(serializedObject, property);
            Init();
        }

        private void Init()
        {
            _reorderableList.drawHeaderCallback = rect =>
            {
                GUI.Label(rect, "子状态机列表");
            };

            _reorderableList.drawElementCallback = (rect, index, active, focused) =>
            {
                SerializedProperty item = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, item, new GUIContent("子状态机" + index),true);
            };

            _reorderableList.onAddCallback = list =>
            {
                list.serializedProperty.arraySize++;
                list.index++;
                SerializedProperty item = list.serializedProperty.GetArrayElementAtIndex(list.index);
                var name = item.FindPropertyRelative("SubMachineName");
                var anis = item.FindPropertyRelative("AnimationObjects");
                name.stringValue = "";
                anis.arraySize = 0;
            };

            _reorderableList.elementHeightCallback = index =>
            {
                var element = _property.GetArrayElementAtIndex(index);
                var name = element.FindPropertyRelative("SubMachineName");
                var anis = element.FindPropertyRelative("AnimationObjects");
                return EditorGUI.GetPropertyHeight(name) + EditorGUI.GetPropertyHeight(anis) + EditorGUIUtility.singleLineHeight + 5;
            };

            _reorderableList.onRemoveCallback = list =>
            {
                if (EditorUtility.DisplayDialog("移除子状态机元素", "是否移除当前元素", "是", "否"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(list);
                }
            };
        }

        public void OnGUI()
        {
            _reorderableList.DoLayoutList();
        }
    }
}
