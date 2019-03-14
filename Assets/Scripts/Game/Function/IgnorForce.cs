using UnityEngine;

namespace Game
{
    public class IgnorForce : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
