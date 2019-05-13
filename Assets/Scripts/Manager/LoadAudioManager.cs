using System;
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
        //第一层key是EnemyID枚举值 value值是当前怪物对应的音效字典
        private Dictionary<string, Dictionary<string, AudioClip>> _enemyClipsDic;

        public void Init()
        {
            _playerClipsDic = new Dictionary<string, AudioClip>();
            LoadAllAudio(Const.Path.PLAYER_AUDIO_PATH, _playerClipsDic);
            _enemyClipsDic = new Dictionary<string, Dictionary<string, AudioClip>>();

            foreach (EnemyId id in Enum.GetValues(typeof(EnemyId)))
            {
                _enemyClipsDic[id.ToString()] = new Dictionary<string, AudioClip>();
                LoadAllAudio(Const.Path.AUDIO_PATH + id+"/", _enemyClipsDic[id.ToString()]);
            }
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

        public AudioClip EnemyAudio(string id,string name)
        {
            if (!_enemyClipsDic[id].ContainsKey(name))
            {
                Debug.LogError("当前查找音效名称不在缓存内，查找名称为："+ name);
                return null;
            }
            else
            {
                return _enemyClipsDic[id][name];
            }
        }
    }
}
