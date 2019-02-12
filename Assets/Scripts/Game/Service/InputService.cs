using System;
using UIFrame;
using UnityEngine;

namespace Game.Service
{
    /// <summary>
    /// 输入服务接口
    /// </summary>
    public interface IInputService
    {
        void Input(InputButton button, InputState state);
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
            _contexts.input.SetGameInputButton(InputButton.NULL,InputState.NULL);
            _contexts.service.SetGameServiceEntitasInputService(this);
        }

        public int GetPriority()
        {
            return 0;
        }

        public void Update()
        {

        }

        public void Input(InputButton button, InputState state)
        {
            _contexts.input.ReplaceGameInputButton(button, state);
        }
    }

    /// <summary>
    /// 与Unity交互的输入服务
    /// </summary>
    public class UnityInputService : IInputService, IInitService,IExecuteService,IPlayerBehaviour
    {
        private IInputService _entitaInputService;
        private bool _isPress;
        private InputButtonComponent _inputButton;

        public void Init(Contexts contexts)
        {
            _inputButton = contexts.input.gameInputButton;
            _entitaInputService = contexts.service.gameServiceEntitasInputService.EntitasInputService;
        }

        public int GetPriority()
        {
            return 1;
        }

        public void Excute()
        {
            _isPress = false;

            Forward();

            Back();

            Left();

            Right();

            Attack(0);

            Idle();
        }
        public void Idle()
        {
            if (!_isPress && _inputButton.InputButton != InputButton.NULL && _inputButton.InputState != InputState.NULL)
            {
                _entitaInputService.Input(InputButton.NULL, InputState.NULL);
            }
        }

        public void Forward()
        {
            if(!InputDown(KeyCode.W, InputButton.FORWARD))
                InputPress(KeyCode.W, InputButton.FORWARD);
        }

        public void Back()
        {
            if(!InputDown(KeyCode.S, InputButton.BACK))
                InputPress(KeyCode.S, InputButton.BACK);
        }

        public void Left()
        {
            if (!InputDown(KeyCode.A, InputButton.LEFT))
                InputPress(KeyCode.A, InputButton.LEFT);
        }

        public void Right()
        {
            if (!InputDown(KeyCode.D, InputButton.RIGHT))
                InputPress(KeyCode.D, InputButton.RIGHT);
        }

        public bool IsRun { get; set; }

        public void Attack(int skillCode)
        {
            InputDown(KeyCode.K, InputButton.ATTACK_O);
            InputDown(KeyCode.L, InputButton.ATTACK_X);
        }

        public bool InputDown(KeyCode code, InputButton button)
        {
            if (UnityEngine.Input.GetKeyDown(code))
            {
                Input(button, InputState.DOWN);
                _isPress = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InputPress(KeyCode code, InputButton button)
        {
            if (UnityEngine.Input.GetKey(code))
            {
                Input(button, InputState.PREE);
                _isPress = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InputUp(KeyCode code, InputButton button)
        {
            if (UnityEngine.Input.GetKeyUp(code))
            {
                Input(button, InputState.UP);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Input(InputButton button, InputState state)
        {
            _entitaInputService.Input(button,state);
        }
    }
}
