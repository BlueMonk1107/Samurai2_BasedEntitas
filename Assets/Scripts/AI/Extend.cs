using BlueGOAP;
using Game.AI.ViewEffect;
using UnityEngine;

namespace Game.AI
{
    public static class Extend     
    {
        /// <summary>
        /// 获取数据类
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="handler"></param>
        /// <param name="maps"></param>
        /// <returns></returns>
        public static TModel GetModel<TModel>(
            this ActionHandlerBase<ActionEnum, GoalEnum> handler,
            IMaps<ActionEnum, GoalEnum> maps
            )
            where TModel : class , IModel
        {
            var mgr = maps.GetGameData<GameDataKeyEnum, AIModelMgr>(GameDataKeyEnum.AI_MODEL_MANAGER);
            return mgr.GetModel<TModel>(handler.Label);
        }
    }
}
