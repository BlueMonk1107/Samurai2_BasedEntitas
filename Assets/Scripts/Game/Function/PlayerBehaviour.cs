using UnityEngine;

namespace Game
{
    /// <summary>
    /// 玩家行为类
    /// </summary>
    public class PlayerBehaviour : IPlayerBehaviour
    {
        private readonly Transform _playerTrans;
        public PlayerBehaviour(Transform player)
        {
            _playerTrans = player;
        }

        public void Forward()
        {
            Move(5, Vector3.forward);
            PlayerOrientation(Vector3.zero);
        }

        public void Back()
        {
            Move(5, Vector3.back);
            PlayerOrientation(Vector3.up * 180);
        }

        public void Left()
        {
            Move(5, Vector3.left);
            PlayerOrientation(Vector3.up * -90);
        }

        public void Right()
        {
            Move(5, Vector3.right);
            PlayerOrientation(Vector3.up * 90);
        }

        public void AttackO()
        {
            throw new System.NotImplementedException();
        }

        public void AttackX()
        {
            throw new System.NotImplementedException();
        }

        private void Move(float speed, Vector3 direction)
        {
            _playerTrans.Translate(Time.deltaTime * speed * direction, Space.World);
        }

        private void PlayerOrientation(Vector3 direction)
        {
            _playerTrans.eulerAngles = direction;
        }
    }
}
