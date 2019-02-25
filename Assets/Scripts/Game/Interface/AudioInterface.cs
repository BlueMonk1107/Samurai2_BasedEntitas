using Game;
using UnityEngine;

namespace Game
{
    public interface AudioInterface
    {
        void Play(string name,float volume);
    }

    public interface IPlayerAudio : AudioInterface, IPlayerBehaviour
    {
        
    }
}
