using System.Collections;
using System.Collections.Generic;
using Const;
using Module;
using UnityEngine;

public class SkillAniState : StateMachineBehaviour
{

    private SkillCodeModule _codeModule;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_codeModule == null)
        {
            _codeModule = new SkillCodeModule();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var clips = animator.GetCurrentAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            int code = _codeModule.GetSkillCode(clips[0].clip.name, "attack", "");
            if (animator.GetInteger(ConstValue.SKILL_PARA_NAME) == code)
            {
                animator.SetInteger(ConstValue.SKILL_PARA_NAME, 0);
            } 
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
