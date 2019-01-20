using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 输入响应系统基类
    /// </summary>
    public abstract class InputButtonSystemBase : ReactiveSystem<InputEntity>
    {
        protected Contexts _contexts;

        public InputButtonSystemBase(Contexts contexts) : base(contexts.input)
        {
            _contexts = contexts;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.GameInputButton);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasGameInputButton && FilterCondition(entity);
        }

        /// <summary>
        /// 事件执行的筛选条件
        /// </summary>
        /// <returns></returns>
        protected abstract bool FilterCondition(InputEntity entity);
    }
    
}
