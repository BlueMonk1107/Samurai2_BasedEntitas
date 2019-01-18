using System;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// 向前按键响应系统
    /// </summary>
    public class InputForwardButtonSystem : InputButtonSystemBase
    {
        public InputForwardButtonSystem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.UP;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.Forward();
        }
    }

    /// <summary>
    /// 向后按键响应系统
    /// </summary>
    public class InputBackButtonSystem : InputButtonSystemBase
    {
        public InputBackButtonSystem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.DOWN;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.Back();
        }
    }

    /// <summary>
    /// 向左按键响应系统
    /// </summary>
    public class InputLeftButtonSystem : InputButtonSystemBase
    {
        public InputLeftButtonSystem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.LEFT;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.Left();
        }
    }

    /// <summary>
    /// 向右按键响应系统
    /// </summary>
    public class InputRightButtonSystem : InputButtonSystemBase
    {
        public InputRightButtonSystem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.RIGHT;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.Right();
        }
    }

    /// <summary>
    /// O攻击按键响应系统
    /// </summary>
    public class InputAttackOButtonSystem : InputButtonSystemBase
    {
        public InputAttackOButtonSystem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.ATTACK_O;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.AttackO();
        }
    }

    /// <summary>
    /// X攻击按键响应系统
    /// </summary>
    public class InputAttackXButtonSystem : InputButtonSystemBase
    {
        public InputAttackXButtonSystem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.ATTACK_X;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.AttackX();
        }
    }

}
