using Manager;
using UnityEngine;

namespace Game.GamePart
{
    public class GamePart : MonoBehaviour
    {
        public void Init(LevelGamePartID id)
        {
            InitPart(id);
        }

        private void InitPart(LevelGamePartID id)
        {
            Part partTemp = null;
            int index = 1;
            foreach (Transform trans in transform)
            {
                partTemp = trans.gameObject.AddComponent<Part>();
                partTemp.Init(id,(LevelPartID)index);
                index++;
            }
        }
    }
}
