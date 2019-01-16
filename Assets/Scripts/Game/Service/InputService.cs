using System;
using UnityEngine;

namespace Game
{
    public interface IInputService
    {
        void Up();
        void Down();
        void Left();
        void Right();

        void AttackO();
        void AttackX();
    }

    public class InputService : IInputService
    {
        public void AttackO()
        {
            throw new NotImplementedException();
        }

        public void AttackX()
        {
            throw new NotImplementedException();
        }

        public void Down()
        {
            throw new NotImplementedException();
        }

        public void Left()
        {
            throw new NotImplementedException();
        }

        public void Right()
        {
            throw new NotImplementedException();
        }

        public void Up()
        {
            throw new NotImplementedException();
        }
    }
}
