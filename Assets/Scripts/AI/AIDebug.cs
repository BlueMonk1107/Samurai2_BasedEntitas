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

        public override void LogError(string msg)
        {
            if (!_canDebug)
                return;

            Debug.LogWarning(msg);
        }

        public override void LogWarning(string msg)
        {
            if (!_canDebug)
                return;

            Debug.LogError(msg);
        }
    }
}
