using System;
using System.Collections.Generic;
using Module;
using Module.Skill;
using UnityEditor.Animations;
using UnityEngine;

namespace Game
{
    public interface ICustomAniEventManager
    {
        void AddEventListener(Action<string> OnStateEnterAction, Action<string> OnStateUpdateAction, Action<string> OnStateExitAction);
    }
    public class CustomAniEventManager : ICustomAniEventManager
    {
        private Dictionary<PlayerAniStateName, AnimatorState> _statesDic;
        private Dictionary<PlayerAniStateName, CustomAniEvent> _eventDic;
        private Animator _animator;

        public CustomAniEventManager(Animator animator)
        {
            _animator = animator;
            _statesDic = new Dictionary<PlayerAniStateName, AnimatorState>();
            _eventDic = new Dictionary<PlayerAniStateName, CustomAniEvent>();
            new InitSkillAni(animator);
            InitAnimatorStateData(animator);
            AddCustomAniEventScripts();
            InitCustomAniEventScripts();
        }

        private void InitAnimatorStateData(Animator animator)
        {
            AnimatorController aniController = animator.runtimeAnimatorController as AnimatorController;
            AnimatorStateMachine aniMachine = aniController.layers[0].stateMachine;


            foreach (ChildAnimatorState state in aniMachine.states)
            {
                foreach (PlayerAniStateName name in Enum.GetValues(typeof(PlayerAniStateName)))
                {
                    if (state.state.name == name.ToString())
                    {
                        _statesDic[name] = state.state;
                    }
                }

                if (!_statesDic.ContainsValue(state.state))
                {
                    Debug.LogError("can not find Enum(PlayerAniStateName) name is " + state.state.name);
                }
            }
        }

        private void AddCustomAniEventScripts()
        {
            CustomAniEvent behaviorTemp;
            foreach (KeyValuePair<PlayerAniStateName, AnimatorState> pair in _statesDic)
            {
                behaviorTemp = null;
                foreach (StateMachineBehaviour behaviour in pair.Value.behaviours)
                {
                    if (behaviour is CustomAniEvent)
                    {
                        behaviorTemp = behaviour as CustomAniEvent;
                        break;
                    }
                }

                if (behaviorTemp == null)
                {
                    _eventDic[pair.Key] = pair.Value.AddStateMachineBehaviour<CustomAniEvent>();
                }
                else
                {
                    _eventDic[pair.Key] = behaviorTemp;
                }
                
            }
        }

        private void InitCustomAniEventScripts()
        {
            foreach (KeyValuePair<PlayerAniStateName, CustomAniEvent> pair in _eventDic)
            {
                pair.Value.Init(pair.Key);
            }
        }

        public void AddEventListener(Action<string> OnStateEnterAction, Action<string> OnStateUpdateAction, Action<string> OnStateExitAction)
        {
            foreach (CustomAniEvent aniEvent in _animator.GetBehaviours<CustomAniEvent>())
            {
                aniEvent.OnStateEnterAction = OnStateEnterAction;
                aniEvent.OnStateUpdateAction = OnStateUpdateAction;
                aniEvent.OnStateExitAction = OnStateExitAction;
            }
        }
    }
    /// <summary>
    /// 技能脚本名称初始化
    /// </summary>
    public class InitSkillAni
    {
        public InitSkillAni(Animator animator)
        {
            SetStateName(animator);
        }

        private void SetStateName(Animator animator)
        {
            SkillCodeModule _codeModule = new SkillCodeModule();

            AnimatorController aniController = animator.runtimeAnimatorController as AnimatorController;
            AnimatorStateMachine aniMachine = aniController.layers[0].stateMachine;

            SkillAniState temp = null;

            foreach (ChildAnimatorState state in aniMachine.stateMachines[0].stateMachine.states)
            {
                foreach (StateMachineBehaviour behaviour in state.state.behaviours)
                {
                    temp = null;
                    if (behaviour is SkillAniState)
                    {
                        temp = behaviour as SkillAniState;
                        break;
                    }
                }

                if (temp != null)
                {
                    int code = _codeModule.GetSkillCode(state.state.name, "attack", "");
                    temp.name = code.ToString();
                }
            }
            
        } 
    }

    /// <summary>
    /// 动画状态名称对应枚举
    /// </summary>
    public enum PlayerAniStateName
    {
        idle,
        walk,
        run,
        idleSword,
        walkSword,
        runSword
    }
}
