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
        private static string _viewPath;
        private static string _servicePath;
        private static string _systemPath;
        private static string _dataPah = "Assets/Editor/AutoGenerateFrameCode/Data/";
        private static string _dataFileName = "Data.asset";
        private static string _viewPostfix = "View";
        private static string _viewName;
        private static string _servicePostfix = "Service";
        private static string _serviceName;
        private static string _systemPostfix = "System";
        private static string _systemName;
        private static string _namespaceBase = "Game";
        private static string[] _contextNames;
        private static Dictionary<string, bool> _contextSelectedState;
        private static string _selectedContextName;

        private static string _otherSystemName;
        private static string[] _systemInterfaceName =
        {
            "IInitializeSystem",
            "IExecuteSystem",
            "ICleanupSystem",
            "ITearDownSystem"
        };
        private static Dictionary<string, bool> _systemSelectedState;
        private static int _lineSpace;

        private GUIStyle _mainTitle;
        private GUIStyle _itemTitle;

        [MenuItem("Tools/GenerateEntitasCode")]
        public static void OpenWindow()
        {
            var window = GetWindow(typeof(GenerateEntitasCode));
            window.minSize = new Vector2(500, 800);
            window.Show();
            Init();
        }

        private static void Init()
        {
            _lineSpace = 15;
            ReadDataFromLocal();
            GetContextName();
            InitContextSelectedState();
            _selectedContextName = _contextNames[0];
            InitSystemSelectedState();
        }

        private static void InitContextSelectedState()
        {
            _contextSelectedState = new Dictionary<string, bool>();

            ResetContextSelectedState();
        }

        private static void InitSystemSelectedState()
        {
            _systemSelectedState = new Dictionary<string, bool>();
            foreach (string system in _systemInterfaceName)
            {
                _systemSelectedState[system] = false;
            }
        }

        private static void ResetContextSelectedState()
        {
            foreach (string contextName in _contextNames)
            {
                _contextSelectedState[contextName] = false;
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("生成Entitas框架代码工具");
           
            Path();

            View();

            Service();

            ReactiveSystem();

            OtherSystems();
        }

        private void Path()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("脚本路径");
            PathItem("View层路径", ref _viewPath);
            PathItem("Service层路径", ref _servicePath);
            PathItem("System层路径", ref _systemPath);

            CreateButton("保存路径", SaveDataToLocal);
        }

        private void View()
        {
            GUILayout.Space(_lineSpace);
            InputName("View层代码生成", ref _viewName);

            CreateButton("生成View脚本", ()=> {});
        }

        private void Service()
        {
            GUILayout.Space(_lineSpace);
            InputName("Service层代码生成", ref _serviceName);

            CreateButton("生成Service脚本", () => { });
        }

        private void ReactiveSystem()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("选择要生成系统的上下文");
            foreach (KeyValuePair<string, bool> pair in _contextSelectedState)
            {
                if (GUILayout.Toggle(pair.Value, pair.Key) && pair.Value == false)
                {
                    _selectedContextName = pair.Key;
                }
            }

            ToggleGroup(_selectedContextName);

            InputName("ReactiveSystem代码生成", ref _systemName);

            CreateButton("生成ReactiveSystem脚本", () => { });
        }

        private void OtherSystems()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("选择要生成的系统");
            foreach (string systemName in _systemInterfaceName)
            {
                _systemSelectedState[systemName] = GUILayout.Toggle(_systemSelectedState[systemName], systemName);
            }

            InputName("系统代码生成", ref _otherSystemName);

            CreateButton("生成系统脚本", () => { });
        }

        //输入要生成脚本的主名称
        private void InputName(string titleName,ref string name)
        {
            GUILayout.Label(titleName);
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(150));
            name = EditorGUI.TextField(rect, name);
        }

        //生成button
        private void CreateButton(string btnName,Action callBack)
        {
            if (GUILayout.Button(btnName, GUILayout.Width(100)))
            {
                callBack?.Invoke();
            }
        }

        private static void ToggleGroup(string name)
        {
            if (_contextSelectedState.ContainsKey(name))
            {
                ResetContextSelectedState();
                _contextSelectedState[name] = true;
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

        //保存数据到本地
        private void SaveDataToLocal()
        {
            Directory.CreateDirectory(_dataPah);
            EntitasData data = new EntitasData();
            data.ViewPath = _viewPath;
            data.ServicePath = _servicePath;
            data.SystemPath = _systemPath;
            AssetDatabase.CreateAsset(data, _dataPah + _dataFileName);
        }

        //从本地读取数据
        private static void ReadDataFromLocal()
        {
            EntitasData data = AssetDatabase.LoadAssetAtPath<EntitasData>(_dataPah + _dataFileName);
            if (data != null)
            {
                _viewPath = data.ViewPath;
                _servicePath = data.ServicePath;
                _systemPath = data.SystemPath;
            }
        }

        /// <summary>
        /// 获取所有上下文名称
        /// </summary>
        private static void GetContextName()
        {
            var provider = new ContextDataProvider();
            provider.Configure(Preferences.sharedInstance);
            var data = (ContextData[])provider.GetData();
            _contextNames = data.Select(u => u.GetContextName()).ToArray();
        }

        private static string GetViewCode()
        {
            var build = new ScriptBuildHelp();
            build.WriteUsing("Entitas");
            build.WriteUsing("Entitas.Unity");
            build.WriteNamespace(_namespaceBase + "." + _viewPostfix);
            build.WriteEmptyLine();
            build.WriteClass(_viewName + _viewPostfix, "ViewBase");

            List<string> keyName = new List<string>();
            keyName.Add("override");
            keyName.Add("void");
            build.WriteFun(keyName, "Init", "Contexts contexts", "IEntity entity");
            build.BackToInsertContent();
            build.WriteLine(" base.Init(contexts, entity);", true);
            build.ToContentEnd();
            return build.ToString();
        }

        private static string GetServiceCode()
        {
            string className = _serviceName + _servicePostfix;

            var build = new ScriptBuildHelp();
            build.WriteNamespace(_namespaceBase + "." + _servicePostfix);
            build.WriteInterface("I" + className, "IInitService");
            build.ToContentEnd();
            build.WriteClass(className, "I" + className);

            build.WriteFun(new List<string>(), "Init", "Contexts contexts");
            build.BackToInsertContent();
            build.WriteLine("//contexts.service.SetGameService" + className + "(this);", true);
            build.ToContentEnd();

            var key = new List<string>();
            key.Add("void");
            build.WriteFun(key, "GetPriority");
            build.BackToInsertContent();
            build.WriteLine("return 0;", true);
            build.ToContentEnd();

            return build.ToString();
        }

        private static string GetReactiveSystemCode()
        {
            string className = _selectedContextName + _systemName + _systemPostfix;
            string entityName = _selectedContextName + "Entity";

            var build = new ScriptBuildHelp();
            build.WriteUsing("Entitas");
            build.WriteNamespace(_namespaceBase);
            build.WriteClass(className, "ReactiveSystem<" + entityName + ">");
            build.WriteLine(" protected Contexts _contexts;", true);
            build.WriteEmptyLine();
            //构造
            build.WriteFun(new List<string>(), className, " : base(context.game)", "Contexts context");
            build.BackToInsertContent();
            build.WriteLine(" _contexts = context;", true);
            build.ToContentEnd();
            //GetTrigger
            List<string> triggerkeys = new List<string>();
            triggerkeys.Add("override");
            triggerkeys.Add("ICollector<" + entityName + ">");
            build.WriteFun("GetTrigger", ScriptBuildHelp.Protected, triggerkeys, "", "IContext<" + entityName + "> context");
            build.BackToInsertContent();
            build.WriteLine("return context.CreateCollector(" + _selectedContextName + "Matcher.Game" + _selectedContextName + _systemName + ");", true);
            build.ToContentEnd();


            //Filter
            List<string> filerkeys = new List<string>();
            filerkeys.Add("override");
            filerkeys.Add("bool");
            build.WriteFun("Filter", ScriptBuildHelp.Protected, filerkeys, "", entityName + " entity");
            build.BackToInsertContent();
            build.WriteLine("return entity.hasGame" + _selectedContextName + _systemName + ";", true);
            build.ToContentEnd();


            //Execute
            List<string> executeKeys = new List<string>();
            executeKeys.Add("override");
            executeKeys.Add("void");
            build.WriteFun("Execute", ScriptBuildHelp.Protected, executeKeys, "", "List<" + entityName + "> entities");

            return build.ToString();
        }

        public static string GetOthersSystemCode()
        {
            string className = _otherSystemName + _systemPostfix;
            List<string> selectedSystem = GetSelectedSysytem();

            var build = new ScriptBuildHelp();
            build.WriteUsing("Entitas");
            build.WriteNamespace(_namespaceBase);
            build.WriteClass(className, GetSelectedSystem(selectedSystem));
            build.WriteLine(" protected Contexts _contexts;", true);
            build.WriteEmptyLine();
            //构造
            build.WriteFun(new List<string>(), className, "", "Contexts context");
            build.BackToInsertContent();
            build.WriteLine(" _contexts = context;", true);
            build.ToContentEnd();
            //实现方法
            List<string> funName = GetFunName(selectedSystem);
            List<string> key = new List<string>();
            key.Add("void");
            foreach (string fun in funName)
            {
                build.WriteFun(key, fun);
            }
            return build.ToString();
        }

        private static List<string> GetSelectedSysytem()
        {
            List<string> temp = new List<string>();
            foreach (KeyValuePair<string, bool> pair in _systemSelectedState)
            {
                if (pair.Value)
                {
                    temp.Add(pair.Key);
                }
            }

            return temp;
        }

        private static string GetSelectedSystem(List<string> selected)
        {
            StringBuilder temp = new StringBuilder();

            foreach (string name in selected)
            {
                temp.Append(name);
                temp.Append(" , ");
            }

            temp.Remove(temp.Length - 4, 3);

            return temp.ToString();
        }

        private static List<string> GetFunName(List<string> selected)
        {
            List<string> temp = new List<string>();

            foreach (string interfaceName in selected)
            {
                temp.Add(interfaceName.Substring(1, interfaceName.Length - 7));
            }

            return temp;
        }
    }
}
