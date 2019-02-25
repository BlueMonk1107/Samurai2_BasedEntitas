using UnityEngine;

namespace Game
{
    public class PlayerAudio : IPlayerAudio
    {
        private AudioSource _audioSource;
        public PlayerAudio(AudioSource source)
        {
            _audioSource = source;
        }

        public void Play(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Idle()
        {
            throw new System.NotImplementedException();
        }

        public void TurnForward()
        {
            throw new System.NotImplementedException();
        }

        public void TurnBack()
        {
            throw new System.NotImplementedException();
        }

        public void TurnLeft()
        {
            throw new System.NotImplementedException();
        }

        public void TurnRight()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRun { get; set; }
        public bool IsAttack { get; }
        public void Attack(int skillCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
