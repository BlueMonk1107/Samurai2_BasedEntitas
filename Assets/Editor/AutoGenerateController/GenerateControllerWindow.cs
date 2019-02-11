using UnityEditor;
using UnityEngine;

namespace CustomTool
{
    public class GenerateControllerWindow : EditorWindow
    {
        public static string OutputPath = "";
        public static bool IsCreateAnimatorController = true;
        private static EditorWindow _window;

        [MenuItem("Tools/GenerateAnimatorController")]
        public static void CreateWindow()
        {
            //绘制窗口
            _window = GetWindow(typeof(GenerateControllerWindow));
            _window.Show();
        }


        private void OnGUI()
        {
            GUILayout.BeginVertical();

            //绘制标题
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Create Prefabs");

            //绘制文本
            GUILayout.Space(10);
            OutputPath = EditorGUILayout.TextField("Output Path:", OutputPath);

            GUILayout.Space(10);
            IsCreateAnimatorController = EditorGUILayout.Toggle("Create AnimaControl:", IsCreateAnimatorController);

            if (GUILayout.Button("生成"))
            {
                GenerateController.SelectedAni();
                _window.Close();
            }

            GUILayout.EndVertical();
        }

    }
}
