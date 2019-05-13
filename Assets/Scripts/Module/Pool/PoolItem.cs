using System;
using UnityEngine;

namespace Game
{
    public class PoolItem : MonoBehaviour     
    {
        public string Name { get; set; }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
