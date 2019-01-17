using UnityEngine;

namespace Game
{
    public class GameFeature : Feature     
    {
        public GameFeature(Contexts contexts)
        {
            Init();
            Excute();
            ReactiveSystem(contexts);
        }

        private void Init()
        {
            
        }

        private void Excute()
        {
            
        }

        private void ReactiveSystem(Contexts contexts)
        {
            Add(new GameStartSystem(contexts));
            Add(new GamePauseSystem(contexts));
            Add(new GameEndSystem(contexts));
        }
    }
}
