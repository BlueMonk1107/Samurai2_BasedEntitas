using System;
using System.Threading.Tasks;
using Const;
using UnityEngine;

namespace Game
{
    public class PlayerAudio : IPlayerAudio
    {
        private AudioSource _audioSource;
        private int _times;
        private bool _isRun;

        public bool IsRun
        {
            get { return _isRun; }
            set
            {
                _times = 0;
                _isRun = value;
            }
        }
        public bool IsAttack { get; }

        public PlayerAudio(AudioSource source)
        {
            _audioSource = source;
            _times = 0;
        }

        public void Play(string name,float volume = 1)
        {
            _audioSource.PlayOneShot(GetAudioClip(name), volume);
        }

        private void Play(AudioName name, float volume = 1)
        {
            Play(name.ToString(), volume);
        }

        private AudioClip GetAudioClip(string name)
        {
            return LoadAudioManager.Single.PlayerAudio(name);
        }

        public void Idle()
        {
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

        public void Move()
        {
            if (_times == 0)
            {
                Play(AudioName.step, ConstValue.MOVE_STEP_VOLUME);
            }
            _times++;
            if (_times >= GetFrames())
            {
                _times = 0;
            }
        }

        private int GetFrames()
        {
            if (IsRun)
            {
                return ConstValue.RUN_STEP_TIME;
            }
            else
            {
                return ConstValue.WALK_STEP_TIME;
            }
        }
     
        public async void Attack(int skillCode)
        {
            await Task.Delay(TimeSpan.FromSeconds(ConstValue.SKILL_START_TIME));
            Play(AudioName.attack);
        }
    }
}
