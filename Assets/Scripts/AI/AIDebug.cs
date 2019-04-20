using System;
using BlueGOAP;
using UnityEngine;

namespace Game.UI
{
    public class AIDebug : DebugMsgBase
    {
        public override void Log(string msg)
        {
            Debug.Log(msg);
        }

        public override void LogError(string msg)
        {
            Debug.LogWarning(msg);
        }

        public override void LogWarning(string msg)
        {
            Debug.LogError(msg);
        }
    }
}
