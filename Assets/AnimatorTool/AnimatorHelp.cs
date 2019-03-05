using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace CustomTool
{
    public class AnimatorHelp : StateMachineBehaviour
    {
        public AnimatorController Controller { get; set; }
        public AnimatorState AnimatorState { get; set; }
        public Dictionary<AnimatorStateTransition, bool> TransitionsDic { get; set; }
        [SerializeField]
        public List<AnimatorStateTransition> TransitionsList { get; set; }
        /// <summary>
        /// Toggle框选中
        /// </summary>
        public bool IsToggleSelect { get; set; }
        /// <summary>
        /// 所有Transition选项被选中
        /// </summary>
        public bool SelectAllTransion { get; set; }
        [MultiSelectEnum]
        public ParaEnum SelectDataToChange;
        public CustomTransitionPara TransitionPara;


        public void InitTransitionsDic()
        {
            if (TransitionsDic != null)
                return;

            TransitionsDic = new Dictionary<AnimatorStateTransition, bool>();
            TransitionsList = new List<AnimatorStateTransition>();
            foreach (AnimatorStateTransition transition in AnimatorState.transitions)
            {
                TransitionsList.Add(transition);
                TransitionsDic[transition] = false;
            }
        }

        public bool GetSelectedData(ParaEnum para)
        {
            return (SelectDataToChange & para) == para;
        }
    }

    [Flags]
    public enum ParaEnum
    {
        hasExitTime = 1,
        exitTime = 2,
        hasFixedDuration = 4,
        duration = 8,
        offset = 16,
        interruptionSource = 32
    }

    [System.Serializable]
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class MultiSelectEnumAttribute : PropertyAttribute
    {
        public MultiSelectEnumAttribute()
        {
            
        }
    }

    [System.Serializable]
    public class CustomTransitionPara
    {
        public bool hasExitTime;
        public float exitTime;
        public bool hasFixedDuration;
        public float duration;
        public float offset;
        public TransitionInterruptionSource interruptionSource;
    }
}
