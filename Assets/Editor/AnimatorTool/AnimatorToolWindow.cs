using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace CustomTool
{
    public class AnimatorToolWindow : EditorWindow
    {
        private static AnimatorToolWindow _window;
        private static string _cachePath = "Assets/Editor/AnimatorTool/Cache/";
        private static string _cacheName = "AnimatorToolCache.asset";
        private static string _aniControllerPath;
        private static string _newAniName;
        [SerializeField]
        public List<GameObject> _animationObjects = new List<GameObject>();
        [SerializeField]
        public List<SubAnimatorMachineItem> _subMachineItems = new List<SubAnimatorMachineItem>(); 

        private static SerializedObject _serializedObject;
        private static SerializedProperty _animations;
        private static SerializedProperty _machineItems;
        private GenerateController _generater;
        private CustomReorderableList _customReorderableList;

        private bool _isAddDefaultAnis;

        [MenuItem("Tools/AnimatorTool %m")]
        public static void ShowWindowInMenu()
        {
            OpenWindow();
        }

        //在工程视图界面下右键菜单
        [MenuItem("Assets/AnimatorTool/Add")]
        public static void ShowWindowInProject()
        {
            AutoAddAniObjects();
        }

        //在工程视图界面下的检测函数
        [MenuItem("Assets/AnimatorTool/Add", true)]
        public static bool ShowWindowInProjectValidate()
        {
            return Selection.activeObject.GetType() == typeof(GameObject);
        }

        //在检视面板在右键菜单
        [MenuItem("GameObject/AnimatorTool", priority = 0)]
        public static void ShowWindowInHierarchy()
        {
            OpenWindow();
        }

        private static void AutoAddAniObjects()
        {
            AddAniObjects(_window._animationObjects, Selection.gameObjects.ToList());

            foreach (SubAnimatorMachineItem item in _window._subMachineItems)
            {
                if (item.IsAutoAdd)
                {
                    AddAniObjects(item.AnimationObjects, Selection.gameObjects.ToList());
                }
            }
        }

        private static void AddAniObjects(List<GameObject> data,List<GameObject> selection)
        {
            foreach (GameObject gameObject in selection)
            {
                if (!data.Contains(gameObject))
                {
                    data.Add(gameObject);
                }
            }
        }

        private static void OpenWindow()
        {
            _window = (AnimatorToolWindow)GetWindow(typeof(AnimatorToolWindow));
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
            _isAddDefaultAnis = true;
            _generater = new GenerateController();
            _serializedObject = new SerializedObject(this);
            _animations = _serializedObject.FindProperty("_animationObjects");
            _machineItems = _serializedObject.FindProperty("_subMachineItems");
            _customReorderableList = new CustomReorderableList(_serializedObject, _machineItems);
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

            UpdateSerializedObject();

            CreateButton("创建", CreateNewController);
            AddAniToggles();
        }

        private void AddAniToggles()
        {
            GUILayout.Space(10);
            GUILayout.Label("选择右键快速导入动画片段的状态机");
            GUILayout.Space(5);
            _isAddDefaultAnis = GUILayout.Toggle(_isAddDefaultAnis, new GUIContent("默认状态机"));

            foreach (SubAnimatorMachineItem item in _subMachineItems)
            {
                item.IsAutoAdd = GUILayout.Toggle(item.IsAutoAdd, new GUIContent(item.SubMachineName));
            }
        }

        private void UpdateSerializedObject()
        {
            _serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            GetAnimationObject();
            _customReorderableList.OnGUI();

            if (EditorGUI.EndChangeCheck())
            {
                _serializedObject.ApplyModifiedProperties();
            }
        }
       
        private void GetAnimationObject()
        {
            EditorGUILayout.PropertyField(_animations,new GUIContent("默认层级动画片段父物体数组"), true);
        }

        private void CreateNewController()
        {
            bool success = _generater.Create(_aniControllerPath, _newAniName, _animationObjects, _subMachineItems);
            if (!success)
            {
                Debug.LogError("获取动画片段失败，无法创建AnimatorController");
            }
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
