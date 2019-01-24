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
        private static string _serviceManagerPath;
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

        private static string _viewFeaturePath;
        private static string _inputFeaturePath;
        private static string _gameFeaturePath;

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

        private static void Close()
        {
            AssetDatabase.Refresh();
            _window.Close();
        }

        private static void Init()
        {
            _lineSpace = 15;
            ReadDataFromLocal();
            GetContextName();
            InitContextSelectedState();
            _selectedContextName = _contextNames[0];
            InitSystemSelectedState();
            InitGuiStyle();
        }

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
            GUILayout.Space(_lineSpace);
            GUILayout.Label("生成Entitas框架代码工具", _mainTitle);
           
            Path();

            View();

            Service();

            SelectContext();

            ReactiveSystem();

            OtherSystems();
        }

        private void Path()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("脚本路径", _itemTitle);
            GUILayout.Space(_lineSpace);

            PathItem("View层路径", ref _viewPath);
            PathItem("Service层路径", ref _servicePath);
            PathItem("System层路径", ref _systemPath);

            GUILayout.Space(_lineSpace);
            PathItem("ServiceManager路径", ref _serviceManagerPath);
            PathItem("ViewFeature路径", ref _viewFeaturePath);
            PathItem("InputFeature路径", ref _inputFeaturePath);
            PathItem("GameFeature路径", ref _gameFeaturePath);

            CreateButton("保存路径", SaveDataToLocal);
        }

        private void View()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("View层代码生成", _itemTitle);
            InputName("请输入脚本名称", ref _viewName);

            CreateButton("生成脚本", () =>
            {
                CreateScript(_viewPath, _viewName + _viewPostfix, GetViewCode());
            });
        }

        private void Service()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("Service层代码生成", _itemTitle);

            InputName("请输入脚本名称", ref _serviceName);

            CreateButton("生成脚本", () =>
            {
                CreateScript(_servicePath, _serviceName + _servicePostfix, GetServiceCode());
                InitServices(_serviceManagerPath);
            });
        }

        private void SelectContext()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("选择生成系统的上下文", _itemTitle);

            GUILayout.BeginHorizontal();
            foreach (KeyValuePair<string, bool> pair in _contextSelectedState)
            {
                if (GUILayout.Toggle(pair.Value, pair.Key) && pair.Value == false)
                {
                    _selectedContextName = pair.Key;
                }
            }
            GUILayout.EndHorizontal();
            ToggleGroup(_selectedContextName);
        }

        private void ReactiveSystem()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("响应系统部分", _itemTitle);

            InputName("请输入脚本名称", ref _systemName);

            CreateButton("生成脚本", () =>
            {
                CreateScript(_systemPath, _systemName + _systemPostfix, GetReactiveSystemCode());
                InitSystem(_selectedContextName,
                    _selectedContextName + _systemName + _systemPostfix,
                    "ReactiveSystem");
            });
        }

        private void OtherSystems()
        {
            GUILayout.Space(_lineSpace);
            GUILayout.Label("其他系统部分", _itemTitle);
            GUILayout.Label("选择要生成的系统", _contentTitle);

            foreach (string systemName in _systemInterfaceName)
            {
                _systemSelectedState[systemName] = GUILayout.Toggle(_systemSelectedState[systemName], systemName);
            }
            GUILayout.Space(_lineSpace);

            InputName("请输入脚本名称", ref _otherSystemName);

            CreateButton("生成脚本", () =>
            {
                CreateScript(_systemPath, _otherSystemName + _systemPostfix, GetOthersSystemCode());
                List<string> selectedSystem = GetSelectedSysytem();
                List<string> funName = GetFunName(selectedSystem);
                InitSystem(_selectedContextName,
                    _selectedContextName + _otherSystemName + _systemPostfix,
                    funName.ToArray());
            });
        }

        private void InitServices(string path)
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                int index = content.IndexOf("IInitService[] services =");
                int newIndex = content.IndexOf("new", index);
                content = content.Insert(newIndex, "new "+ _serviceName + _servicePostfix + "(),\r                ");
                File.WriteAllText(path, content,Encoding.UTF8);

                Close();
            }
            else
            {
                Debug.LogError("Service脚本不存在");
            }
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
                    Close();
                    callBack?.Invoke();
                }
                else
                {
                    Debug.LogError("名称不能为空");
                }
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
            data.ServiceManagerPath = _serviceManagerPath;
            data.GameFeaturePath = _gameFeaturePath;
            data.InputFeaturePath = _inputFeaturePath;
            data.ViewFeaturePath = _viewFeaturePath;
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
                _serviceManagerPath = data.ServiceManagerPath;
                _gameFeaturePath = data.GameFeaturePath;
                _inputFeaturePath = data.InputFeaturePath;
                _viewFeaturePath = data.ViewFeaturePath;
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
            build.WriteEmptyLine();

            build.WriteNamespace(_namespaceBase + "." + _viewPostfix);

            build.IndentTimes++;
            build.WriteClass(_viewName + _viewPostfix, "ViewBase");

            build.IndentTimes++;
            List<string> keyName = new List<string>();
            keyName.Add("override");
            keyName.Add("void");
            build.WriteFun(keyName, "Init","","Contexts contexts", "IEntity entity");

            build.BackToInsertContent();
            build.IndentTimes++;
            build.WriteLine(" base.Init(contexts, entity);", true);
            build.ToContentEnd();

            return build.ToString();
        }

        private static string GetServiceCode()
        {
            string className = _serviceName + _servicePostfix;

            var build = new ScriptBuildHelp();
            build.WriteNamespace(_namespaceBase + "." + _servicePostfix);
            //interface
            build.IndentTimes++;
            build.WriteInterface("I" + className, "IInitService");
            build.ToContentEnd();
            //class
            build.WriteClass(className, "I" + className);
            //init函数
            build.IndentTimes++;
            List<string> initKey = new List<string>();
            initKey.Add("void");
            build.WriteFun(initKey, "Init","", "Contexts contexts");
            //init 内容
            build.BackToInsertContent();
            build.IndentTimes++;
            build.WriteLine("//contexts.service.SetGameService" + className + "(this);", true);
            build.IndentTimes--;
            build.ToContentEnd();
            //GetPriority函数
            var key = new List<string>();
            key.Add("int");
            build.WriteFun(key, "GetPriority");

            build.BackToInsertContent();
            build.IndentTimes++;
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
            build.WriteUsing("System.Collections.Generic");
            build.WriteNamespace(_namespaceBase);

            build.IndentTimes++;
            build.WriteClass(className, "ReactiveSystem<" + entityName + ">");

            build.IndentTimes++;
            build.WriteLine(" protected Contexts _contexts;", true);
            build.WriteEmptyLine();
            //构造
            build.WriteFun(new List<string>(), className, " : base(context.game)", "Contexts context");
            build.BackToInsertContent();
            build.IndentTimes++;
            build.WriteLine(" _contexts = context;", true);
            build.IndentTimes--;
            build.ToContentEnd();
            //GetTrigger
            List<string> triggerkeys = new List<string>();
            triggerkeys.Add("override");
            triggerkeys.Add("ICollector<" + entityName + ">");
            build.WriteFun("GetTrigger", ScriptBuildHelp.Protected, triggerkeys, "", "IContext<" + entityName + "> context");
            build.BackToInsertContent();
            build.IndentTimes++;
            build.WriteLine("return context.CreateCollector(" + _selectedContextName + "Matcher.Game" + _selectedContextName + _systemName + ");", true);
            build.IndentTimes--;
            build.ToContentEnd();


            //Filter
            List<string> filerkeys = new List<string>();
            filerkeys.Add("override");
            filerkeys.Add("bool");
            build.WriteFun("Filter", ScriptBuildHelp.Protected, filerkeys, "", entityName + " entity");
            build.BackToInsertContent();
            build.IndentTimes++;
            build.WriteLine("return entity.hasGame" + _selectedContextName + _systemName + ";", true);
            build.IndentTimes--;
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
            string className = _selectedContextName + _otherSystemName + _systemPostfix;
            List<string> selectedSystem = GetSelectedSysytem();

            var build = new ScriptBuildHelp();
            build.WriteUsing("Entitas");
            build.WriteNamespace(_namespaceBase);

            build.IndentTimes++;
            build.WriteClass(className, GetSelectedSystem(selectedSystem));

            build.IndentTimes++;
            build.WriteLine("protected Contexts _contexts;", true);
            build.WriteEmptyLine();
            //构造
            build.WriteFun(new List<string>(), className, "", "Contexts context");
            build.BackToInsertContent();
            build.IndentTimes++;
            build.WriteLine("_contexts = context;", true);
            build.IndentTimes--;
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

        private static void InitSystem(string contextName,string className,params string[] systemName)
        {
            string path = "";
            switch (contextName)
            {
                case "Game":
                    path = _gameFeaturePath;
                    break;
                case "Input":
                    path = _inputFeaturePath;
                    break;
            }

            if (string.IsNullOrEmpty(path))
                return;

            foreach (string s in systemName)
            {
                SetSystem(path, s, className);
            }

            Close();
        }

        private static void SetSystem(string path,string systemName,string className)
        {
            
            string content = File.ReadAllText(path);
            int index = content.IndexOf("void " + systemName + "Fun(Contexts contexts)");
            if (index < 0)
            {
                Debug.LogError("未找到对应方法，系统名："+ systemName);
                return;
            }

            int startIndex = content.IndexOf("{",index);
            content = content.Insert(startIndex + 1, "\r            Add(new " + className + "(contexts));");
            File.WriteAllText(path,content,Encoding.UTF8);
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

            temp.Remove(temp.Length - 2, 2);

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

        private static void CreateScript(string path,string className,string scriptContent)
        {
            if (Directory.Exists(path))
            {
                File.WriteAllText(path+"/"+className+".cs", scriptContent, Encoding.UTF8);
            }
            else
            {
                Debug.LogError("目录:"+path+"不存在");
            }
        }
    }
}
