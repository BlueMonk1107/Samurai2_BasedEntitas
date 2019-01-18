using Entitas;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// View²ã½Ó¿Ú
    /// </summary>
    public interface IView
    {
        void Init(Contexts contexts,IEntity entity);
    }
}
