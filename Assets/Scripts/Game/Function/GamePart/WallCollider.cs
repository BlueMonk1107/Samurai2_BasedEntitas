using UnityEngine;

namespace Game
{
    public class WallCollider : MonoBehaviour
    {
        private Collider _collider;

        public void Init(Collider collider)
        {
            _collider = collider;
        }

        public void SetWallState(bool isOpen)
        {
            _collider.enabled = !isOpen;
        }
    }
}
