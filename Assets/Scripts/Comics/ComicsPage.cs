using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UIFrame
{
    public class ComicsPage : MonoBehaviour     
    {
        private Sprite[] numSprits;
        private Image _indexImage;

        public void Start()
        {
            numSprits = GetComponent<NumSprites>().numSprits;
            _indexImage = transform.Find("Index").Image();
        }

        public void ShowNum(int index)
        {
            if (index >= numSprits.Length)
            {
                Debug.LogError("index > numSprits length");
                return;
            }
            else
            {
                _indexImage.sprite = numSprits[index];
            }
        }
    }
}
