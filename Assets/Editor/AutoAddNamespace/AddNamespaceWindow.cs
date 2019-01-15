using UnityEngine;
using UnityEditor;
using System.IO;

namespace CustomTool
{
    public class AddNamespaceWindow : EditorWindow
    {
        private static string _name;
        private static string _path = "Assets/Editor/AutoAddNamespace/Cache/";
        private static string _dataName = "Data.asset";

        [MenuItem("Tools/AddNamespace")]
        public static void OpenWindow()
        {
            var window = GetWindow(typeof (AddNamespaceWindow));
            window.minSize = new Vector2(500,300);
            window.Show();
            Init();
        }

        public static NamespaceData GetData()
        {
            return AssetDatabase.LoadAssetAtPath<NamespaceData>(_path + _dataName);
        }

        private static void Init()
        {
            NamespaceData data = GetData();
            if (data != null)
            {
                _name = data.name;
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("命名空间名称");
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(200));
            _name = EditorGUI.TextField(rect, _name);

            if(GUILayout.Button("完成", GUILayout.MaxWidth(100)))
            {
                NamespaceData data = new NamespaceData();
                data.name = _name;

                Directory.CreateDirectory(_path);
                AssetDatabase.CreateAsset(data, _path+_dataName);
            }
        }
    }
}
