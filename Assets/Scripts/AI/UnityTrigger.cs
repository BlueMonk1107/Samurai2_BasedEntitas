using System;
using System.Threading.Tasks;
using Const;
using Game.View;
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
            EnemyPeasantView view = GetComponent<EnemyPeasantView>();
            view.AIAgent.Maps.SetGameData(GameDataKeyEnum.INJURE_VALUE, 1);
            ////上方向
            //_colliderAction(GetDirectionCollider(center, 20));
            //await Task.Delay(TimeSpan.FromSeconds(1));
            //右方向
            //_colliderAction(GetDirectionCollider(center, 35));
            //await Task.Delay(TimeSpan.FromSeconds(1));
            ////左方向
            //_colliderAction(GetDirectionCollider(center, -35));
            //await Task.Delay(TimeSpan.FromSeconds(1));
            ////下方向
            //_colliderAction(GetDirectionCollider(center, 160));
            //await Task.Delay(TimeSpan.FromSeconds(1));

            

            //普通死亡
            //view.AIAgent.Maps.SetGameData(GameDataKeyEnum.INJURE_VALUE, 100);
            //_colliderAction(GetPosCollider(new Vector3(center.x, center.y, center.z)));
            //await Task.Delay(TimeSpan.FromSeconds(2));

            //view.AIAgent.Maps.SetGameData(GameDataKeyEnum.INJURE_VALUE, 1000);
            ////头部
            //_colliderAction(GetPosCollider(new Vector3(center.x, center.y + controller.height * 0.5f - 0.3f, center.z)));
            //await Task.Delay(TimeSpan.FromSeconds(2));
            //身体
            //_colliderAction(GetPosCollider(new Vector3(center.x, center.y, center.z)));
            //await Task.Delay(TimeSpan.FromSeconds(2));
            ////腿
            //_colliderAction(GetPosCollider(new Vector3(center.x, center.y - controller.height * 0.5f + 0.3f, center.z)));
        }

        private Collider GetDirectionCollider(Vector3 center,float degress)
        {
            GameObject go = new GameObject("TestObject");
            go.tag = TagAndLayer.WEAPON_TAG;
            Collider c = go.AddComponent<BoxCollider>();
            c.transform.position = center + new Vector3(Mathf.Sin(Mathf.Deg2Rad * degress), Mathf.Cos(Mathf.Deg2Rad * degress), 0);
            return c;
        }

        private Collider GetPosCollider(Vector3 pos)
        {
            GameObject go = new GameObject("TestObject");
            go.tag = TagAndLayer.WEAPON_TAG;
            Collider c = go.AddComponent<BoxCollider>();
            c.transform.position = pos;
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
