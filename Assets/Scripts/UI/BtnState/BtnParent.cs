using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public class BtnParent : MonoBehaviour     
    {
        public int Index { get; private set; }
        public int ChildCount {
            get { return transform.childCount; }
        }
        private List<SelectedBtn> childs;
        private int _childId;

        public void Init(int index)
        {
            Index = index;
            _childId = 0;
            childs = new List<SelectedBtn>();
            foreach (Transform trans in transform)
            {
                childs.Add(trans.gameObject.AddComponent<SelectedBtn>());
            }
        }

        public void SelectedDefaut()
        {
            Selected(childs[0].transform);
        }

        public void Selected(Transform selected)
        {
            var btn = selected.GetComponentInChildren<SelectedBtn>();
            if (btn != null)
            {
                btn.Selected();
            }
        }

        public void CancelSelected()
        {
            
        }

        public bool Left()
        {
            _childId--;
            if (_childId >= 0)
            {
                Selected(childs[_childId].transform);
                return true;
            }
            else
            {
                _childId = 0;
                return false;
            }
           
        }

        public bool Right()
        {
            _childId++;
            if (_childId < ChildCount)
            {
                Selected(childs[_childId].transform);
                return true;
            }
            else
            {
                _childId = ChildCount - 1;
                return false;
            }
        }
    }
}
