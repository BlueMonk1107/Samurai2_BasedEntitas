using System;
using UnityEngine;

namespace Game.Service
{
    /// <summary>
    /// 输入服务接口
    /// </summary>
    public interface IInputService : IPlayerBehaviour
    {

    }
    /// <summary>
    /// 与Entitas交互的输入服务
    /// </summary>
    public class EntitasInputService : IInputService,IInitService
    {
        private Contexts _contexts;

        public void Init(Contexts contexts)
        {
            _contexts = contexts;
            _contexts.input.SetGameInputButton(InputButton.NULL);
            _contexts.game.SetGameEntitasInputService(this);
        }

        public void Update()
        {

        }

        public void AttackO()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.ATTACK_O);
        }

        public void AttackX()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.ATTACK_X);
        }

        public void Back()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.DOWN);
        }

        public void Left()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.LEFT);
        }

        public void Right()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.RIGHT);
        }

        public void Idle()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.NULL);
        }

        public void Forward()
        {
            _contexts.input.ReplaceGameInputButton(InputButton.UP);
        }
    }

    /// <summary>
    /// 与Unity交互的输入服务
    /// </summary>
    public class UnityInputService : IInputService, IInitService,IExecuteService
    {
        private IInputService _entitaInputService;
        private bool _isPress;

        public void Init(Contexts contexts)
        {
            _entitaInputService = contexts.game.gameEntitasInputService.EntitasInputService;
        }

        public void Excute()
        {
            _isPress = false;

            Forward();

            Back();

            Left();

            Right();

            AttackO();

            AttackX();

            Idle();
        }
        public void Idle()
        {
            if (!_isPress)
            {
                _entitaInputService.Idle();
            }
        }

        public void Forward()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _entitaInputService.Forward();
                _isPress = true;
            }
        }

        public void Back()
        {
            if (Input.GetKey(KeyCode.S))
            {
                _entitaInputService.Back();
                _isPress = true;
            }
        }

        public void Left()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _entitaInputService.Left();
                _isPress = true;
            }
        }

        public void Right()
        {
            if (Input.GetKey(KeyCode.D))
            {
                _entitaInputService.Right();
                _isPress = true;
            }
        }

        public void AttackO()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                _entitaInputService.AttackO();
                _isPress = true;
            }
        }

        public void AttackX()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                _entitaInputService.AttackX();
                _isPress = true;
            }
        }
    }
}
