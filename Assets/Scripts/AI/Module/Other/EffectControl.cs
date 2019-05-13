using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class EffectControl : MonoBehaviour  
    {
        public void Play()
        {
            float duration = 0;

            var emits = GetComponentsInChildren<EllipsoidParticleEmitter>();
            foreach (EllipsoidParticleEmitter emitter in emits)
            {
                emitter.Emit();

                if (emitter.maxEnergy > duration)
                {
                    duration = emitter.maxEnergy;
                }
            }

            if (emits != null && emits.Length > 0)
            {
                AutoHide(duration);
            }
        }

        private async void AutoHide(float duration)
        {
            await Task.Delay(TimeSpan.FromSeconds(duration));
            PoolItem item = gameObject.GetComponent<PoolItem>();
            PoolManager.Single.EffectPool.Despwan(item.Name, item);
        }
    }
}
