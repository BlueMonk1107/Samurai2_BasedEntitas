using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public abstract class TriggerBase : TriggerBase<ActionEnum, GoalEnum>
    {
        public TriggerBase(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public TClass GetGameData<TClass>(GameDataKeyEnum key) where TClass : class
        {
            return _agent.Maps.GetGameData<GameDataKeyEnum, TClass>(key);
        }

        public TValue GetGameDataValue<TValue>(GameDataKeyEnum key) where TValue : struct
        {
            return _agent.Maps.GetGameDataValue<GameDataKeyEnum, TValue>(key);
        }

        public void SetGameData(GameDataKeyEnum key,object value)
        {
            _agent.Maps.SetGameData(key, key);
        }
    }
}
