using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace CustomTool
{
    public class AnimatorToolWindow : EditorWindow
    {
        private static EditorWindow _window;
        private static string _cachePath = "Assets/Editor/AnimatorTool/Cache/";
        private static string _cacheName = "AnimatorToolCache.asset";
        private static string _aniControllerPath;
        private static string _newAniName;
        [SerializeField]
        private List<GameObject> _animationObjects = new List<GameObject>();

        private static SerializedObject _serializedObject;
        private static SerializedProperty _animations;
        private GenerateController _generater;

        [MenuItem("Tools/AnimatorTool %m")]
        public static void ShowWindowInMenu()
        {
            OpenWindow();
        }

        //在工程视图界面下右键菜单
        [MenuItem("Assets/AnimatorTool")]
        public static void ShowWindowInProject()
        {
            OpenWindow();
        }

        //在工程视图界面下的检测函数
        [MenuItem("Assets/AnimatorTool", true)]
        public static bool ShowWindowInProjectValidate()
        {
            return Selection.activeObject.GetType() == typeof(AnimatorController)
                || Selection.activeObject.GetType() == typeof(GameObject);
        }

        //在检视面板在右键菜单
        [MenuItem("GameObject/AnimatorTool", priority = 0)]
        public static void ShowWindowInHierarchy()
        {
            OpenWindow();
        }

        private static void OpenWindow()
        {
            _window = GetWindow(typeof(AnimatorToolWindow));
            _window.minSize = new Vector2(500, 800);
            _window.Show();
            Init();
        }

        private static void Init()
        {
            ReadDataFromLocal();
        }

        private void InitAnimationList()
        {
            _animationObjects = Selection.gameObjects.ToList();
        }

        private void OnEnable()
        {
            _generater = new GenerateController();
            _serializedObject = new SerializedObject(this);
            _animations = _serializedObject.FindProperty("_animationObjects");
            InitAnimationList();
        }


        //路径UI显示及输入
        private void PathItem(string name, ref string path)
        {
            GUILayout.Label(name);
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(150));
            path = EditorGUI.TextField(rect, path);
            DragToGetPath(rect, ref path);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            PathItem("AnimatorController存放路径", ref _aniControllerPath);
            CreateButton("保存", SaveDataToLocal);
            GUILayout.Space(10);
            InputName("新建AnimatorController名称", ref _newAniName);
            GetAnimationObject();
            CreateButton("创建", CreateNewController);
        }

       
        private void GetAnimationObject()
        {
            _serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_animations, true);

            if (EditorGUI.EndChangeCheck())
            {
                _serializedObject.ApplyModifiedProperties();
            }
        }

        private void CreateNewController()
        {
            List<AnimationClip> clips = GetAnimationClip();
            if (clips != null && clips.Count > 0)
            {
                _generater.Create(_aniControllerPath,_newAniName, clips);
            }
            else
            {
                Debug.LogError("获取动画片段失败，无法创建AnimatorController");
            }
        }


        /// <summary>
        /// 获取动画片段文件
        /// </summary>
        private List<AnimationClip> GetAnimationClip()
        {
            if (_animationObjects.Count == 0)
                return null;

            List<AnimationClip> clips = new List<AnimationClip>();

            foreach (GameObject gameObject in _animationObjects)
            {
                string path = AssetDatabase.GetAssetPath(gameObject);
                var assets = AssetDatabase.LoadAllAssetsAtPath(path);
                foreach (UnityEngine.Object asset in assets)
                {
                    if (asset is AnimationClip)
                    {
                        AnimationClip clip = asset as AnimationClip;
                        if (!clip.name.Contains("Take"))
                        {
                            clips.Add(clip);
                        }
                    }
                }
            }

            return clips;
        }

        //拖动文件夹获取路径
        private void DragToGetPath(Rect rect, ref string path)
        {
            if ((Event.current.type == EventType.DragUpdated
                 || Event.current.type == EventType.DragExited)
                && rect.Contains(Event.current.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
                {
                    path = DragAndDrop.paths[0];
                }
            }
        }

        //生成button
        private void CreateButton(string btnName, Action callBack)
        {
            if (GUILayout.Button(btnName, GUILayout.Width(100)))
            {
                if (!string.IsNullOrEmpty(btnName))
                {
                    callBack?.Invoke();
                    Close();
                }
                else
                {
                    Debug.LogError("名称不能为空");
                }
            }
        }

        //输入要生成脚本的主名称
        private void InputName(string titleName, ref string name)
        {
            GUILayout.Label(titleName);
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(150));
            name = EditorGUI.TextField(rect, name);
        }

        //保存数据到本地
        public static void SaveDataToLocal()
        {
            Directory.CreateDirectory(_cachePath);
            AnimatorToolData data = new AnimatorToolData();
            data.AnimatorControllerPath = _aniControllerPath;
            AssetDatabase.CreateAsset(data, _cachePath + _cacheName);
        }

        //从本地读取数据
        private static void ReadDataFromLocal()
        {
            AnimatorToolData data = AssetDatabase.LoadAssetAtPath<AnimatorToolData>(_cachePath + _cacheName);
            if (data != null)
            {
                _aniControllerPath = data.AnimatorControllerPath;
            }
        }
    }
}
