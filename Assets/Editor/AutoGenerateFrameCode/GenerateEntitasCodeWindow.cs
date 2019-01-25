using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DesperateDevs.Serialization;
using Entitas.CodeGeneration.Plugins;
using UnityEditor;
using UnityEngine;

namespace CustomTool
{
    /// <summary>
    /// 生成Entitas框架代码工具
    /// </summary>
    public class GenerateEntitasCode : EditorWindow
    {

        private static int _lineSpace;

        private static GUIStyle _mainTitle;
        private static GUIStyle _itemTitle;
        private static GUIStyle _contentTitle;

        private static EditorWindow _window;

        [MenuItem("Tools/GenerateEntitasCode")]
        public static void OpenWindow()
        {
            _window = GetWindow(typeof(GenerateEntitasCode));
            _window.minSize = new Vector2(500, 800);
            _window.Show();
            Init();
        }
        /// <summary>
        /// 关闭插件窗口
        /// </summary>
        private static void Close()
        {
            AssetDatabase.Refresh();
            _window.Close();
        }
        /// <summary>
        /// 初始化插件
        /// </summary>
        private static void Init()
        {
            _lineSpace = 15;
            ToolData.Init();
            InitGuiStyle();
        }
        /// <summary>
        /// 初始化UIstyle
        /// </summary>
        private static void InitGuiStyle()
        {
            _mainTitle = new GUIStyle();
            _mainTitle.alignment = TextAnchor.MiddleCenter;
            _mainTitle.normal.textColor = Color.green;
            _mainTitle.fontSize = 30;
            _mainTitle.fontStyle = FontStyle.Bold;

            _itemTitle = new GUIStyle();
            _itemTitle.normal.textColor = Color.yellow;
            _itemTitle.fontSize = 15;
            _itemTitle.fontStyle = FontStyle.Bold;

            _contentTitle = new GUIStyle();
            _contentTitle.normal.textColor = Color.white;
            _contentTitle.fontSize = 10;
        }
        /// <summary>
        /// 绘制ui系统函数
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("生成Entitas框架代码工具", _mainTitle);
           
            Path();

            View();

            Service();

            SelectContext();

            ReactiveSystem();

            OtherSystems();
        }
        /// <summary>
        /// 路径部分UI
        /// </summary>
        private void Path()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("脚本路径", _itemTitle);
            GUILayout.Space(_lineSpace);

            PathItem("View层路径", ref ToolData.ViewPath);
            PathItem("Service层路径", ref ToolData.ServicePath);
            PathItem("System层路径", ref ToolData.SystemPath);

            GUILayout.Space(_lineSpace);
            PathItem("ServiceManager路径", ref ToolData.ServiceManagerPath);
            PathItem("ViewFeature路径", ref ToolData.ViewFeaturePath);
            PathItem("InputFeature路径", ref ToolData.InputFeaturePath);
            PathItem("GameFeature路径", ref ToolData.GameFeaturePath);

            CreateButton("保存路径", ToolData.SaveDataToLocal);
        }
        /// <summary>
        /// View部分UI
        /// </summary>
        private void View()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("View层代码生成", _itemTitle);
            InputName("请输入脚本名称", ref ToolData.ViewName);

            CreateButton("生成脚本", () =>
            {
                GenerateCode.CreateScript(ToolData.ViewPath, ToolData.ViewName + ToolData.ViewPostfix, CodeTemplate.GetViewCode());
            });
        }
        /// <summary>
        /// Service部分UI
        /// </summary>
        private void Service()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("Service层代码生成", _itemTitle);

            InputName("请输入脚本名称", ref ToolData.ServiceName);

            CreateButton("生成脚本", () =>
            {
                GenerateCode.CreateScript(ToolData.ServicePath, ToolData.ServiceName + ToolData.ServicePostfix, CodeTemplate.GetServiceCode());
                GenerateCode.InitServices(ToolData.ServiceManagerPath);
                Close();
            });
        }
        /// <summary>
        /// 选择上下文部分UI
        /// </summary>
        private void SelectContext()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("选择生成系统的上下文", _itemTitle);

            GUILayout.BeginHorizontal();
            foreach (KeyValuePair<string, bool> pair in ToolData.ContextSelectedState)
            {
                if (GUILayout.Toggle(pair.Value, pair.Key) && pair.Value == false)
                {
                    ToolData.SelectedContextName = pair.Key;
                }
            }
            GUILayout.EndHorizontal();
            ToggleGroup(ToolData.SelectedContextName);
        }
        /// <summary>
        /// 响应系统部分UI
        /// </summary>
        private void ReactiveSystem()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("响应系统部分", _itemTitle);

            InputName("请输入脚本名称", ref ToolData.ReactiveSystemName);

            CreateButton("生成脚本", () =>
            {
                GenerateCode.CreateScript(ToolData.SystemPath, ToolData.ReactiveSystemName + ToolData.SystemPostfix, CodeTemplate.GetReactiveSystemCode());
                GenerateCode.InitSystem(ToolData.SelectedContextName,
                    ToolData.SelectedContextName + ToolData.ReactiveSystemName + ToolData.SystemPostfix,
                    "ReactiveSystem");
                Close();
            });
        }
        /// <summary>
        /// 其他系统部分UI
        /// </summary>
        private void OtherSystems()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("其他系统部分", _itemTitle);
            GUILayout.Label("选择要生成的系统", _contentTitle);

            foreach (string systemName in ToolData.SystemInterfaceName)
            {
                ToolData.SystemSelectedState[systemName] = GUILayout.Toggle(ToolData.SystemSelectedState[systemName], systemName);
            }
            GUILayout.Space(_lineSpace);

            InputName("请输入脚本名称", ref ToolData.OtherSystemName);

            CreateButton("生成脚本", () =>
            {
                GenerateCode.CreateScript(ToolData.SystemPath, ToolData.OtherSystemName + ToolData.SystemPostfix, CodeTemplate.GetOthersSystemCode());
                List<string> selectedSystem = CodeTemplate.GetSelectedSysytem();
                List<string> funName = CodeTemplate.GetFunName(selectedSystem);
                GenerateCode.InitSystem(ToolData.SelectedContextName,
                    ToolData.SelectedContextName + ToolData.OtherSystemName + ToolData.SystemPostfix,
                    funName.ToArray());
                Close();
            });
        }
     
        //输入要生成脚本的主名称
        private void InputName(string titleName,ref string name)
        {
            GUILayout.Label(titleName, _contentTitle);
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(150));
            name = EditorGUI.TextField(rect, name);
        }

        //生成button
        private void CreateButton(string btnName,Action callBack)
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
        //单选toggle组
        private static void ToggleGroup(string name)
        {
            if (ToolData.ContextSelectedState.ContainsKey(name))
            {
                ToolData.ResetContextSelectedState();
                ToolData.ContextSelectedState[name] = true;
            }
        }

        //路径UI显示及输入
        private void PathItem(string name, ref string path)
        {
            GUILayout.Label(name);
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(150));
            path = EditorGUI.TextField(rect, path);
            DragToGetPath(rect, ref path);
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

    }
}
