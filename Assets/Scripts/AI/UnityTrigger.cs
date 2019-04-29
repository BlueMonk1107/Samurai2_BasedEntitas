using System;
using System.Threading.Tasks;
using Const;
using UnityEngine;

namespace Game.AI
{
    public class UnityTrigger : MonoBehaviour
    {
        private Action<Collider> _colliderAction;

#if TEST
        private void Start()
        {
            Test();
        }

        private async void Test()
        {
            var controller = GetComponent<CharacterController>();
            var center = controller.center;
            await Task.Delay(TimeSpan.FromSeconds(1));
            //上方向
            _colliderAction(GetCollider(center, 20));
            await Task.Delay(TimeSpan.FromSeconds(1));
            //右方向
            _colliderAction(GetCollider(center, 35));
            await Task.Delay(TimeSpan.FromSeconds(1));
            //左方向
            _colliderAction(GetCollider(center, -35));
            await Task.Delay(TimeSpan.FromSeconds(1));
            //下方向
            _colliderAction(GetCollider(center, 160));
        }

        private Collider GetCollider(Vector3 center,float degress)
        {
            GameObject go = new GameObject();
            go.tag = TagAndLayer.WEAPON_TAG;
            Collider c = go.AddComponent<BoxCollider>();
            c.transform.position = center + new Vector3(Mathf.Sin(Mathf.Deg2Rad * degress), Mathf.Cos(Mathf.Deg2Rad * degress), 0);
            
            return c;
        }
        #endif

        private void OnTriggerEnter(Collider other)
        {
            if (_colliderAction != null)
                _colliderAction(other);
        }

        public void AddCollideListener(Action<Collider> colliderAction)
        {
            _colliderAction += colliderAction;
        }
    }
}
