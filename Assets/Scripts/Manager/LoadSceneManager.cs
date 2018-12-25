using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIFrame
{
    public class LoadSceneManager : SingletonBase<LoadSceneManager>
    {
        private AsyncOperation _operation;

        public float Progress
        {
            get { return _operation.progress; }
        }

        public async void AllowSwitchScene()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            _operation.allowSceneActivation = true;
        }

        public IEnumerator LoadSceneAsync(string name)
        {
            _operation = SceneManager.LoadSceneAsync(name);
            _operation.allowSceneActivation = false;
            yield return _operation;
        }
    }
}
