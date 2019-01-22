using System.Collections.Generic;
using Entitas;

namespace Game
{
    /// <summary>
    /// 游戏状态监听系统基类
    /// </summary>
    public abstract class GameStateSystemBase : ReactiveSystem<GameEntity>
    {
        protected Contexts _contexts;

        public GameStateSystemBase(Contexts context) : base(context.game)
        {
            _contexts = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.GameGameState);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGameGameState && FilterCondition(entity);
        }

        protected abstract bool FilterCondition(GameEntity entity);
    }

    /// <summary>
    /// 游戏开始响应事件
    /// </summary>
    public class GameStartSystem : GameStateSystemBase
    {
        public GameStartSystem(Contexts context) : base(context)
        {
        }

        protected override bool FilterCondition(GameEntity entity)
        {
            return entity.gameGameState.GameState == GameState.START;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _contexts.service.gameServiceLoadService.LoadService.LoadPlayer();
            _contexts.game.ReplaceGameCameraState(CameraAniName.START_GAME_ANI);
        }
    }

    /// <summary>
    /// 游戏暂停响应事件
    /// </summary>
    public class GamePauseSystem : GameStateSystemBase
    {
        public GamePauseSystem(Contexts context) : base(context)
        {
        }

        protected override bool FilterCondition(GameEntity entity)
        {
            return entity.gameGameState.GameState == GameState.PAUSE;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 游戏结束响应事件
    /// </summary>
    public class GameEndSystem : GameStateSystemBase
    {
        public GameEndSystem(Contexts context) : base(context)
        {
        }

        protected override bool FilterCondition(GameEntity entity)
        {
            return entity.gameGameState.GameState == GameState.END;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
