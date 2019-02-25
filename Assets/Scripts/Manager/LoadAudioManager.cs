using System.Collections.Generic;
using Manager;
using UIFrame;
using UnityEngine;

namespace Game
{
    public class LoadAudioManager : SingletonBase<LoadAudioManager>
    {
        //玩家音效缓存
        private Dictionary<string, AudioClip> _playerClipsDic;

        public void Init()
        {
            _playerClipsDic = new Dictionary<string, AudioClip>();
            LoadAllAudio(Const.Path.PLAYER_AUDIO_PATH, _playerClipsDic);
        }

        private void LoadAllAudio(string path, Dictionary<string, AudioClip> clipsDic)
        {
            var clips = LoadManager.Single.LoadAll<AudioClip>(path);
            foreach (AudioClip clip in clips)
            {
                clipsDic[clip.name] = clip;
            }
        }

        public AudioClip PlayerAudio(string name)
        {
            if (_playerClipsDic.ContainsKey(name))
            {
                return _playerClipsDic[name];
            }
            else
            {
                Debug.LogError("人物音效中未发现名为"+name+"的音效片段");
                return null;
            }
        }
    }
}
