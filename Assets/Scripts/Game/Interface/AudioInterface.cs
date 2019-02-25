using Game;
using UnityEngine;

namespace Game
{
    public interface AudioInterface
    {
        void Play(string name);
    }

    public interface IPlayerAudio : AudioInterface, IPlayerBehaviour
    {
        
    }
}
