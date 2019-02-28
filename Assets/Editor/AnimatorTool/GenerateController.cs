using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Util;

namespace CustomTool
{
    public class GenerateController
    {
        public bool Create(string path, string name, List<GameObject> animationObjects, List<SubAnimatorMachineItem> subMachineItems)
        {
            AnimatorController ctrl = AnimatorController.CreateAnimatorControllerAtPath(path + "/" + name + ".controller");
            AnimatorStateMachine machine = ctrl.GetAnimatorStateMachine(0);

            bool successCreate = AddDefaultLayerClips(machine, animationObjects);
            AddSubMachines(machine, subMachineItems);

            return successCreate;
        }

        private bool AddDefaultLayerClips(AnimatorStateMachine machine, List<GameObject> animationObjects)
        {
            List<AnimationClip> clips = GetAnimationClip(animationObjects);
            AddAnimationClips(machine, clips);

            return clips != null && clips.Count > 0;
        }

        private void AddSubMachines(AnimatorStateMachine machine, List<SubAnimatorMachineItem> subMachineItems)
        {
            int times = 0;
            AnimatorStateMachine tempMachine;
            List<AnimationClip> tempClips;
            foreach (SubAnimatorMachineItem item in subMachineItems)
            {
                tempMachine = AddSubMachine(machine, item, times);
                if (tempMachine != null)
                {
                    tempClips = GetAnimationClip(item.AnimationObjects);
                    AddAnimationClips(tempMachine, tempClips);
                }
                times++;
            }
        }

        private AnimatorStateMachine AddSubMachine(AnimatorStateMachine machine, SubAnimatorMachineItem subMachineItems, int times)
        {
            return machine.AddStateMachine(subMachineItems.SubMachineName,
                new Vector3(300 * (times / 5), -(100 * (times % 5) + 100), 0));
        }

        private void AddAnimationClips(AnimatorStateMachine machine, List<AnimationClip> clips)
        {
            if(clips == null || clips.Count == 0)
                return;

            int times = 0;
            AnimatorState tempState;
            foreach (AnimationClip clip in clips)
            {
                tempState = machine.AddState(clip.name, new Vector3(300 * (times / 5), 100 * (times % 5) + 300, 0));
                tempState.motion = clip;
                times++;
            }
        }

        /// <summary>
        /// 获取动画片段文件
        /// </summary>
        private List<AnimationClip> GetAnimationClip(List<GameObject> animationObjects)
        {
            if (animationObjects == null || animationObjects.Count == 0)
                return null;

            List<AnimationClip> clips = new List<AnimationClip>();

            foreach (GameObject gameObject in animationObjects)
            {
                string path = AssetDatabase.GetAssetPath(gameObject);
                var assets = AssetDatabase.LoadAllAssetsAtPath(path);
                foreach (UnityEngine.Object asset in assets)
                {
                    if (asset is AnimationClip)
                    {
                        AnimationClip clip = asset as AnimationClip;
                        if (!clip.name.Contains("Take"))
                        {
                            clips.Add(clip);
                        }
                    }
                }
            }

            return clips;
        }
    }
}
