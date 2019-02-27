using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Util;

namespace CustomTool
{
    public class GenerateController     
    {
        public void Create(string path,string name,List<AnimationClip> clips)
        {
            AnimatorController ctrl = AnimatorController.CreateAnimatorControllerAtPath(path+"/"+ name+ ".controller");
            AnimatorStateMachine machine = ctrl.GetAnimatorStateMachine(0);
            AddAnimationClips(machine, clips);
        }

        private void AddAnimationClips(AnimatorStateMachine machine, List<AnimationClip> clips)
        {
            int times = 0;
            AnimatorState tempState;
            foreach (AnimationClip clip in clips)
            {
                tempState = machine.AddState(clip.name, new Vector3(300 * (times / 5), 100 * (times % 5)+300,0));
                tempState.motion = clip;
                times ++;
            }
            
        }
    }
}
