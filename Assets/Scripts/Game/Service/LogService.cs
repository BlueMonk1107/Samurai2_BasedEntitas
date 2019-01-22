using System;
using UnityEngine;

namespace Game.Service
{
    public interface ILogService : IInitService
    {
        void Log(string message);
        void LogError(string message);
    }

    public class LogService : ILogService
    {
        public void Init(Contexts contexts)
        {
            contexts.service.SetGameServiceLogService(this);
        }

        public int GetPriority()
        {
            return 0;
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

      
    }
}
