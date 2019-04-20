
namespace BlueGOAP
{
    public abstract class DebugMsgBase
    {
        public static DebugMsgBase Instance { get; set; }

        public abstract void Log(string msg);

        public abstract void LogWarning(string msg);

        public abstract void LogError(string msg);
    }

    public class DebugMsg
    {
        public static void Log(string msg)
        {
            DebugMsgBase.Instance.Log(msg);
        }

        public static void LogWarning(string msg)
        {
            DebugMsgBase.Instance.LogWarning(msg);
        }

        public static void LogError(string msg)
        {
            DebugMsgBase.Instance.LogError(msg);
        }
    }
}
