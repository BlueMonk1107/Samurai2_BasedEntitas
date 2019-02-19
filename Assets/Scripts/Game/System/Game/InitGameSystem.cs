using Entitas;

namespace Game
{
    //初始化游戏
    public class GameInitGameSystem : IInitializeSystem
    {
        protected Contexts _contexts;

        public GameInitGameSystem(Contexts context)
        {
            _contexts = context;
        }
        public void Initialize()
        {
            _contexts.service.gameServiceLoadService.LoadService.LoadPlayer();
        }
    }
}
