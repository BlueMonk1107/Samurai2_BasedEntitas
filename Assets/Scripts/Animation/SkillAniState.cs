using System;
using System.Collections;
using System.Collections.Generic;
using Const;
using Module;
using UnityEngine;

public class SkillAniState : StateMachineBehaviour
{
    private int _code = -1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //(Clone)
        if (_code < 0)
        {
            try
            {
                string code = name.Remove(name.Length - 7, 7);
                _code = int.Parse(code);
            }
            catch (Exception)
            {
               Debug.LogError("转换技能编码出错");
                _code = 0;
            }
           
        }
       
        Contexts.sharedInstance.game.ReplaceGameStartHumanSkill(_code);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Contexts.sharedInstance.game.ReplaceGameEndHumanSkill(_code);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
