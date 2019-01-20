using Const;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Íæ¼Ò¶¯»­Àà
    /// </summary>
    public class PlayerAni : IPlayerAni
    {
        private Animator _ani;
        public PlayerAni(Animator animator)
        {
            _ani = animator;
        }

        public void Play(int aniIndex)
        {
            _ani.SetInteger(ConstValue.PLAYER_PARA_NAME, aniIndex);
        }

        public void Idle()
        {
            Play(PlayerAniIndex.IDLE);
        }

        private void Play(PlayerAniIndex index)
        {
            Play((int) index);
        }

        public void Forward()
        {
            Move();
        }

        public void Back()
        {
            Move();
        }

        public void Left()
        {
            Move();
        }

        public void Right()
        {
            Move();
        }

        public void AttackO()
        {
            throw new System.NotImplementedException();
        }

        public void AttackX()
        {
            throw new System.NotImplementedException();
        }

        private void Move()
        {
            Play(PlayerAniIndex.WALK);
        }
    }
}
