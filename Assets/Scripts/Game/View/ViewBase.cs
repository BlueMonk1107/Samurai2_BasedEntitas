using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// View²ã»ùÀà
    /// </summary>
    public abstract class ViewBase : MonoBehaviour, IView
    {
        protected GameEntity _entity;
        public virtual void Init(Contexts contexts, IEntity entity)
        {
            gameObject.Link(entity, contexts.game);
            if (entity is GameEntity)
            {
                _entity = (GameEntity) entity;
            }
        }
    }
}
