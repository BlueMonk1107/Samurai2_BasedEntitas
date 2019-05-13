using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class SpawnView
    {
        public void Play(EffectMgr effectMgr,AudioMgr audioMgr, object selfTrans)
        {
            Transform self = selfTrans as Transform;
            effectMgr.Play(EffectNameEnum.Spawn, self.position);
            audioMgr.Play(AudioNameEnum.spawn, AudioVolumeConst.SPAWN_VOLUME);
        }
    }
}
