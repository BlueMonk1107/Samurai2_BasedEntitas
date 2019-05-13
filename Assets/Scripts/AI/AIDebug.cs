using System;
using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class AIDebug : DebugMsgBase
    {
        private bool _canDebug = false;

        public override void Log(string msg)
        {
            if(!_canDebug)
                return;

            Debug.Log(msg);
        }

        public override void LogWarning(string msg)
        {
            Debug.LogWarning(msg);
        }

        public override void LogError(string msg)
        {
            Debug.LogError(msg);
        }
    }
}
