using BlueGOAP;
using Entitas;
using Entitas.Unity;
using Game.AI;
using Manager;
using UnityEngine;

namespace Game.View
{
    public class EnemyPeasantView : ViewBase
    {
        private IAgent<ActionEnum, GoalEnum> _ai;

        public override void Init(Contexts contexts,IEntity entity)         
        {
             base.Init(contexts, entity);
             _ai = new PeasantAgent();
            EnemyData temp = ModelManager.Single.EnemyDataModel.DataDic[EnemyId.EnemyPeasant];
            EnemyData data = new EnemyData();
            data.Copy(temp);
            _ai.Maps.SetGameData(GameDataKeyEnum.CONFIG, data);
            _ai.Maps.SetGameData(GameDataKeyEnum.SELF_TRANS,transform);
            Transform player = (contexts.game.gamePlayer.Player as ViewBase).transform;
            _ai.Maps.SetGameData(GameDataKeyEnum.ENEMY_TRANS, player);
        }

        private void FixedUpdate()
        {
            _ai.FrameFun();
        }
    }
}
