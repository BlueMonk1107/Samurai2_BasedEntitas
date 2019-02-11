using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace CustomTool
{
    public class GenerateController
    {
        public static void SelectedAni()
        {
            Object[] _objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Unfiltered);
            if (_objs.Length == 0)
            {
                Debug.Log("你没有选择任何物体！");
                return;
            }
            for (int i = 0; i < _objs.Length; i++)
            {
                if (!Directory.Exists(Application.dataPath + "/" + GenerateControllerWindow.OutputPath))
                {
                    Directory.CreateDirectory(Application.dataPath + "/" + GenerateControllerWindow.OutputPath);
                }
                GameObject _temObJ = _objs[i] as GameObject;
                string _outputPath = "";
                if (string.IsNullOrEmpty(GenerateControllerWindow.OutputPath))
                {
                    _outputPath = "Assets/";
                }
                else
                {
                    _outputPath = "Assets/" + GenerateControllerWindow.OutputPath + "/";
                }

                if (GenerateControllerWindow.IsCreateAnimatorController)
                {
                   CreateAnimatorController(AssetDatabase.GetAssetPath(_objs[i]), _temObJ.name + ".controller", _outputPath + "AnimatorControllers");
                }
            }
            AssetDatabase.Refresh();
        }
        private static AnimatorController CreateAnimatorController(string _assetsPath, string _controllerName, string _outPutPath)
        {
            //创建AnimatorController文件，保存在_outPutPath路径下
            if (!Directory.Exists(_outPutPath))
            {
                Directory.CreateDirectory(_outPutPath);
            }

            //生成动画控制器（AnimatorController）
            AnimatorController _animatorController = AnimatorController.CreateAnimatorControllerAtPath(_outPutPath + "/" + _controllerName);

            //添加参数（parameters）
            _animatorController.AddParameter("Normal", AnimatorControllerParameterType.Float);
            _animatorController.AddParameter("Play", AnimatorControllerParameterType.Bool);

            //得到它的Layer， 默认layer为base,可以拓展
            AnimatorControllerLayer _layer = _animatorController.layers[0];

            //把动画文件保存在我们创建的AnimatorController中
            AddStateTransition(_assetsPath, _layer);
            return _animatorController;
        }
        private static void AddStateTransition(string _assetsPath, AnimatorControllerLayer _layer)
        {
            //添加动画状态机（这里只是通过层得到根状态机，并未添加）
            AnimatorStateMachine _stateMachine = _layer.stateMachine;

            // 根据动画文件读取它的AnimationClip对象
            var _datas = AssetDatabase.LoadAllAssetsAtPath(_assetsPath);
            if (_datas.Length == 0)
            {
                Debug.Log(string.Format("Can't find clip in {0}", _assetsPath));
                return;
            }

            // 遍历读取模型中包含的动画片段
            foreach (var _data in _datas)
            {
                if (!(_data is AnimationClip))
                {
                    continue;
                }
                AnimationClip _newClip = _data as AnimationClip;
                AddAnimationClip(_stateMachine, _newClip);
            }

            // 先添加一个默认的空状态
            AnimatorState _emptyState = _stateMachine.AddState("Empty", new Vector3(_stateMachine.entryPosition.x + 200, _stateMachine.entryPosition.y, 0));
        }

        private static void AddAnimationClip(AnimatorStateMachine _stateMachine,AnimationClip clip)
        {
            // 添加与动画名称对应的装态（AnimatorState）到状态机中（AnimatorStateMachine）中，并设置状态
            AnimatorState _startState = _stateMachine.AddState(clip.name, new Vector3(_stateMachine.entryPosition.x + 500, _stateMachine.entryPosition.y + 100, 0));
            _startState.motion = clip;

            

            //_animatorStateTransition.AddCondition(AnimatorConditionMode.If, 0, "BoolParameter"); 为True
            //_animatorStateTransition.AddCondition(AnimatorConditionMode.IfNot, 0, "BoolParameter"); 为False
        }
        //设置Any状态的链接（只能是any往外连，其他状态不能连入any）
        private static void SetAnyTransition(AnimatorStateMachine _stateMachine,AnimatorState to, TransitionData[] datas)
        {
            AnimatorStateTransition transition = _stateMachine.AddAnyStateTransition(to);
            SetTransitionData(transition, datas);
        }

        //设置两个状态的链接
        private static void SetTransition(AnimatorState from, AnimatorState to, TransitionData[] datas)
        {
            AnimatorStateTransition transition = from.AddTransition(to);
            SetTransitionData(transition,datas);
        }
        //设置链接数据
        private static void SetTransitionData(AnimatorStateTransition transition,TransitionData[] datas)
        {
            foreach (TransitionData data in datas)
            {
                transition.AddCondition(data.ConditionMode, data.ConditionValue, data.ParaName);
                transition.hasExitTime = data.HasExitTime;
            }
        }
    }

    /// <summary>
    /// 动画状态链接数据
    /// </summary>
    public struct TransitionData
    {
        /// <summary>
        /// 链接模式
        /// <para>If--------bool型条件为true满足条件</para>
        /// <para>IfNot-----bool型条件为false满足条件</para>
        /// <para>其他的就是正常的比较数值</para>
        /// </summary>
        public AnimatorConditionMode ConditionMode;
        public float ConditionValue;
        public string ParaName;
        public bool HasExitTime;
    }
}
