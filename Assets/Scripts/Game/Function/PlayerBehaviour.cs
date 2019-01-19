using Model;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 玩家行为类
    /// </summary>
    public class PlayerBehaviour : IPlayerBehaviour
    {
        private readonly Transform _playerTrans;
        private readonly PlayerDataModel _model;
        public PlayerBehaviour(Transform player, PlayerDataModel model)
        {
            _playerTrans = player;
            _model = model;
        }

        public void Forward()
        {
            Move(Vector3.forward);
            PlayerOrientation(Vector3.zero);
        }

        public void Back()
        {
            Move(Vector3.back);
            PlayerOrientation(Vector3.up * 180);
        }

        public void Left()
        {
            Move(Vector3.left);
            PlayerOrientation(Vector3.up * -90);
        }

        public void Right()
        {
            Move(Vector3.right);
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

        private void Move(Vector3 direction)
        {
            _playerTrans.Translate(Time.deltaTime * _model.Speed * direction, Space.World);
        }

        private void PlayerOrientation(Vector3 direction)
        {
            _playerTrans.eulerAngles = direction;
        }
    }
}
