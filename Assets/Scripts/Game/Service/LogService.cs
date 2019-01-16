using System;
using UnityEngine;

namespace Game
{
    public interface ILogService
    {
        void Log(string message);
        void LogError(string message);
    }

    public class LogService : ILogService
    {
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
