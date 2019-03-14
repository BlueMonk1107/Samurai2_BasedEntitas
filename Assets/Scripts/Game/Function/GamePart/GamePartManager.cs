using UnityEngine;

namespace Game.GamePart
{
    public class GamePartManager : MonoBehaviour     
    {
        public void Start()
        {
            GamePart partTemp  = null;
            int index = 1;
            foreach (Transform tran in transform)
            {
                partTemp = tran.gameObject.AddComponent<GamePart>();
                partTemp.Init((LevelGamePartID)index);
                index ++;
            }
        }
    }
}
