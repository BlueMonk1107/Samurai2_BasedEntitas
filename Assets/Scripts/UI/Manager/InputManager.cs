using UnityEngine;

namespace UIFrame
{
    public class InputManager : MonoBehaviour     
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RootManager.Instance.Back();
            }

            BtnSelected();
        }

        private void BtnSelected()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RootManager.Instance.ButtonLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RootManager.Instance.ButtonRight();
            }
        }
    }
}
