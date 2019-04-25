using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AIAniMgr
    {
        private Animation _ani;

        public AIAniMgr(object ani)
        {
            _ani = ani as Animation;
        }

        public void Play(string aniName)
        {
            _ani.CrossFade(aniName);
        }

        public float GetAniLength(string aniName)
        {
            return _ani[aniName].length;
        }
    }
}
