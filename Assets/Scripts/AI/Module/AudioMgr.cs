using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AudioMgr
    {
        private AudioSource _audioSource;
        private string _enemyID;

        public AudioMgr(string enemyID,object audioSource)
        {
            _audioSource = audioSource as AudioSource;
            _enemyID = enemyID;
        }

        public void Play<T>(T audioName,float volume)
        {
            AudioClip clip = GetAudioClip(audioName.ToString());
            if(clip == null)
                return;
            _audioSource.PlayOneShot(clip, volume);
        }

        private AudioClip GetAudioClip(string name)
        {
            return LoadAudioManager.Single.EnemyAudio(_enemyID, name);
        }
    }
}
