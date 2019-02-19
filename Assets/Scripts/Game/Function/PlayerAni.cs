using System;
using Const;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Íæ¼Ò¶¯»­Àà
    /// </summary>
    public class PlayerAni : IPlayerAni
    {
        public bool IsRun { get; set; }
        
        private Animator _ani;
        public PlayerAni(Animator animator, ICustomAniEventManager aniEventManager)
        {
            _ani = animator;
            AniEventManager = aniEventManager;
        }

        public void Play(int aniIndex)
        {
            _ani.SetInteger(ConstValue.PLAYER_PARA_NAME, aniIndex);
        }

        public ICustomAniEventManager AniEventManager { get; set; }

        public void Idle()
        {
            Play(PlayerAniIndex.IDLE);
        }

        private void Play(PlayerAniIndex index)
        {
            Play((int) index);
        }

        public void TurnForward()
        {
        }

        public void TurnBack()
        {
        }

        public void TurnLeft()
        {
        }

        public void TurnRight()
        {
        }

        public void Attack(int skillCode)
        {
            _ani.SetInteger(ConstValue.SKILL_PARA_NAME, skillCode);
            _ani.SetBool(ConstValue.IDLE_SWORD_PARA_NAME,true);
        }

        public void Move()
        {
            if (IsRun)
            {
                Play(PlayerAniIndex.RUN);
            }
            else
            {
                Play(PlayerAniIndex.WALK);
            }
            
        }
    }
}
