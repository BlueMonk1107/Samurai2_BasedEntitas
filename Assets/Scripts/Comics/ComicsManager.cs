using System;
using System.Collections.Generic;
using Const;
using UnityEngine;
using System.Linq;
using Manager;
using Util;

namespace UIFrame
{
    public class ComicsManager : MonoBehaviour
    {
        private readonly Dictionary<ComicsParentId, Transform> _parentDic = new Dictionary<ComicsParentId, Transform>();
        private readonly Stack<ComicsItem> _leftStack = new Stack<ComicsItem>();
        private readonly Stack<ComicsItem> _rightStack = new Stack<ComicsItem>();
        private ComicsPage _comicsPage;

        public void Start()
        {
            InitParent();
            InitButtons();
            SpawnItem();
            InitPage();
            InitBgAudio();
        }

        private void InitBgAudio()
        {
            var manager = gameObject.AddComponent<UIAudioManager>();
            manager.Init(Path.BG_AUDIO_PATH, LoadManager.Single.LoadAll<AudioClip>);
            manager.PlayBg(BgAudioName.Level_Bg.ToString());
        }

        private void InitPage()
        {
            _comicsPage = transform.GetByName("Page").gameObject.AddComponent<ComicsPage>();
        }

        private void SpawnItem()
        {
            var sprites = GetSprites();

            SpawnCurrentItem(sprites);

            SpawnRightItem(sprites);
        }

        private void SpawnCurrentItem(List<Sprite> sprites)
        {
            if (_parentDic[ComicsParentId.CurrentComics].childCount == 0)
            {
                InitItem(_parentDic[ComicsParentId.CurrentComics], sprites[0], 0);
            }
        }

        private void SpawnRightItem(List<Sprite> sprites)
        {
            ComicsItem item;
            for (int i = sprites.Count -1; i > 0 ; i--)
            {
                item = InitItem(_parentDic[ComicsParentId.RightComics], sprites[i], i);
                _rightStack.Push(item);
            }
        }

        private ComicsItem InitItem(Transform parnet,Sprite sprite,int index)
        {
            GameObject temp = LoadManager.Single.LoadAndInstaniate(Path.COMICS_ITEM_PREFAB_PATH, parnet);
            ComicsItem item = temp.AddComponent<ComicsItem>();
            item.Init(sprite, index);
            return item;
        }

        private List<Sprite> GetSprites()
        {
            string path = Path.COMICS_PATH + ((int)DataManager.Single.LevelIndex).ToString("00");
            return LoadManager.Single.LoadAll<Sprite>(path).ToList();
        }

        private void InitParent()
        {
            Transform parent = transform.GetByName("Parent");
            if (parent == null)
                return;

            Transform temp;
            foreach (ComicsParentId id in Enum.GetValues(typeof(ComicsParentId)))
            {
                var list = from Transform child in parent where child.name.Contains(id.ToString()) select child;
                temp = list.FirstOrDefault();
                if (temp == null)
                {
                    Debug.LogError("can not find child name:" + id);
                    continue;
                }
                else
                {
                    _parentDic[id] = temp;
                }
            }
        }

        private void InitButtons()
        {
            transform.AddBtnListener("Back", Back);
            transform.AddBtnListener("Left", LeftBtn);
            transform.AddBtnListener("Right", RightBtn);
            transform.AddBtnListener("Done", () =>
            {
                StartCoroutine(LoadSceneManager.Single.LoadSceneAsync(DataManager.Single.GetSceneName()));
                LoadSceneManager.Single.AllowSwitchScene();
            });
        }

        private void LeftBtn()
        {
            if(_rightStack.Count == 0)
                return;

            var item = Move(ComicsParentId.LeftComics);
            _comicsPage.ShowNum(item.Page);
        }

        private void RightBtn()
        {
            if (_leftStack.Count == 0)
                return;

            var item = Move(ComicsParentId.RightComics);
            _comicsPage.ShowNum(item.Page);
        }

        private void Back()
        {
            ComicsItem temp;

            temp = GetCurrntItem();
            ResetToRight(temp);

            int count = _leftStack.Count;
            for (int i = 0; i < count; i++)
            {
                temp = _leftStack.Pop();
                ResetToRight(temp);
            }

            temp = _rightStack.Pop();
            temp.SetParnetAndPosition(_parentDic[ComicsParentId.CurrentComics]);
            _comicsPage.ShowNum(temp.Page);
        }

        private void ResetToRight(ComicsItem item)
        {
            item.SetParnetAndPosition(_parentDic[ComicsParentId.RightComics]);
            _rightStack.Push(item);
        }

        private ComicsItem Move(ComicsParentId id)
        {
            ComicsItem current = GetCurrntItem();
            ComicsItem side = null;
            switch (id)
            {
                case ComicsParentId.LeftComics:
                    _leftStack.Push(current);
                    side = _rightStack.Pop();
                    
                    break;
                case ComicsParentId.RightComics:
                    _rightStack.Push(current);
                    side = _leftStack.Pop();
                    break;
            }
            current.Move(_parentDic[id]);
            side.Move(_parentDic[ComicsParentId.CurrentComics]);

            return side;
        }

        private ComicsItem GetCurrntItem()
        {
            return _parentDic[ComicsParentId.CurrentComics].GetChild(0).GetComponent<ComicsItem>();
        }
    }
}
