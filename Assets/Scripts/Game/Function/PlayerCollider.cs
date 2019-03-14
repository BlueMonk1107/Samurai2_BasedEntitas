using Const;
using UnityEngine;

namespace Game
{
    public class PlayerCollider : MonoBehaviour     
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagAndLayer.WALL_TAG)
            {
                Contexts.sharedInstance.game.gamePlayer.Behaviour.IsCollideWall = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == TagAndLayer.WALL_TAG)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
                {
                    var hasWall = hit.transform.tag == TagAndLayer.WALL_TAG;
                    Contexts.sharedInstance.game.gamePlayer.Behaviour.IsCollideWall = hasWall;
                }
                else
                {
                    Contexts.sharedInstance.game.gamePlayer.Behaviour.IsCollideWall = false;
                }
                
            }
        }
    }
}
