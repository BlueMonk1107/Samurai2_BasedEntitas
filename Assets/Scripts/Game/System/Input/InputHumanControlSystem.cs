using System;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// 没有按键按下的状态
    /// </summary>
    public class InputNullSysytem : InputButtonSystemBase
    {
        public InputNullSysytem(Contexts contexts) : base(contexts)
        {
        }

        protected override bool FilterCondition(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.NULL;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            if (_contexts.game.hasGamePlayer)
            {
                _contexts.game.gamePlayer.Ani.Idle();
            }
        }
    }

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
            _contexts.game.gamePlayer.Ani.Forward();
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
            _contexts.game.gamePlayer.Ani.Back();
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
            _contexts.game.gamePlayer.Ani.Left();
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
            _contexts.game.gamePlayer.Ani.Right();
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
            _contexts.game.gamePlayer.Ani.AttackO();
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
            _contexts.game.gamePlayer.Ani.AttackX();
        }
    }

}
