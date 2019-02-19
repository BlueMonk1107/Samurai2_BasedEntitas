using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 人物技能管理类
    /// </summary>
    public class GameSkillManagerSystem : IInitializeSystem, IGameValidHumanSkillListener, IGameEndHumanSkillListener
    {
        protected Contexts _contexts;
        private Queue<int> _codeCache;
        //缓存指令最大数量
        private int _cacheLengthMax;
        private int _currentPlayingCode;

        public GameSkillManagerSystem(Contexts context)
        {
            _contexts = context;
            _codeCache = new Queue<int>();
            _cacheLengthMax = 2;
        }

        public void Initialize()
        {
            var entity = _contexts.game.CreateEntity();
            entity.AddGameValidHumanSkillListener(this);
            entity.AddGameEndHumanSkillListener(this);
        }

        public void OnGameValidHumanSkill(GameEntity entity, int SkillCode)
        {
            AddCode(SkillCode);

            if (!_contexts.game.gamePlayer.Behaviour.IsAttack)
            {
                PlaySkill();
            }
        }

        private void AddCode(int SkillCode)
        {
            if (_codeCache.Count < _cacheLengthMax)
            {
                _codeCache.Enqueue(SkillCode);
            }
        }

        private bool PlaySkill()
        {
            if(_codeCache.Count <= 0)
                return false;

            int code = _codeCache.Dequeue();
            _currentPlayingCode = code;
            _contexts.game.ReplaceGamePlayHumanSkill(_currentPlayingCode);

            return true;
        }

        public void OnGameEndHumanSkill(GameEntity entity, int SkillCode)
        {
            if (_currentPlayingCode == SkillCode)
            {
                bool playFailed = !PlaySkill();

                if (playFailed)
                {
                    _contexts.game.ReplaceGamePlayHumanSkill(0);
                }
            }
        }
    }
}
