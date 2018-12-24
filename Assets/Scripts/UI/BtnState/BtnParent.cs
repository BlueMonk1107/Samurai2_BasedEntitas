using System;
using System.Collections.Generic;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class BtnParent : MonoBehaviour     
    {
        public SelectedState SelectedState
        {
            set
            {
                ResetChildState();

                if (value == SelectedState.SELECTED)
                {
                    childs[_childId].SelectedState = SelectedState.SELECTED;
                }
            }
        }

        public int Index { get; private set; }

        public int ChildCount
        {
            get { return transform.childCount; }
        }

        private List<SelectedBtn> childs;
        private int _childId;

        public void Init(int index)
        {
            Index = index;
            _childId = 0;
            childs = new List<SelectedBtn>();
            SelectedBtn temp;
            foreach (Transform trans in transform)
            {
                temp = trans.gameObject.AddComponent<SelectedBtn>();
                childs.Add(temp);
                temp.AddSelectActionListener(SelectButtonMouse);
            }
        }

        private void SelectButtonMouse(SelectedBtn btn)
        {
            _childId = btn.Index;
            ResetChildState();
            btn.SelectedState = SelectedState.SELECTED;
        }

        public void SelectedButton()
        {
            childs[_childId].SelectedButton();
        }

        public void SelectedDefaut()
        {
            Selected(childs[0].transform);
        }

        private void Selected(Transform selected)
        {
            var btn = selected.GetComponentInChildren<SelectedBtn>();
            if (btn != null)
            {
                btn.Selected();
            }
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

        private void ResetChildState()
        {
            foreach (SelectedBtn child in childs)
            {
                child.SelectedState = SelectedState.UNSELECTED;
            }
        }
    }
}
