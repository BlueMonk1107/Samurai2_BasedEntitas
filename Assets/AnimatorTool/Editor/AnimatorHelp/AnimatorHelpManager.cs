using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Util;

namespace CustomTool
{
    public class AnimatorHelpManager
    {
        private static AnimatorHelpManager _instance;
        public static AnimatorHelpManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AnimatorHelpManager();
                }
                return _instance;
            }
        }

        public AnimatorController Add()
        {
            AnimatorController controller = Selection.activeObject as AnimatorController;
            AddHelp(controller, controller.GetAllAnimatorStates());

            return controller;
        }

        private void AddHelp(AnimatorController controller, List<AnimatorState> states)
        {
            bool has = false;
            foreach (AnimatorState state in states)
            {
                has = false;
                foreach (StateMachineBehaviour behaviour in state.behaviours)
                {
                    if (behaviour is AnimatorHelp)
                    {
                        has = true;
                        break;
                    }
                }

                if (!has)
                {
                    var help = state.AddStateMachineBehaviour<AnimatorHelp>();
                    help.name = controller.name +"#"+state.nameHash;
                }
            }
        }
    }
}
