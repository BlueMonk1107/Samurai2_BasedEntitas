using System.Collections.Generic;
using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class AIModelMgrBase<T>
    {
        private Dictionary<T, IModel> _modelsDic;

        public AIModelMgrBase()
        {
            _modelsDic = new Dictionary<T, IModel>();
            InitModels();
        }

        public TModel GetModel<TModel>(T label) where TModel : class , IModel
        {
            if (!_modelsDic.ContainsKey(label))
            {
                return null;
            }
            else
            {
                return _modelsDic[label] as TModel;
            }
        }

        protected abstract void InitModels();

        protected void AddModel(T label, IModel model)
        {
            if (!_modelsDic.ContainsKey(label))
            {
                _modelsDic.Add(label, model);
            }
            else
            {
                DebugMsg.LogError("缓存中未找到，未对该Model对象进行初始化，标签:" + label);
            }
        }
    }

    public class AIModelMgr : AIModelMgrBase<ActionEnum>
    {
        public AIModelMgr() : base() { }

        protected override void InitModels()
        {
            AddModel(ActionEnum.ATTACK, new AttackModel());
            AddModel(ActionEnum.ENTER_ALERT, new EnterAlertModel());
            AddModel(ActionEnum.EXIT_ALERT, new ExitAlertModel());
        }
    }
}
