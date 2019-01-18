using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Íæ¼ÒÔ¤ÖÆView
    /// </summary>
    public class PlayerView : ViewService
    {
        public override void Init(Contexts contexts, IEntity entity)
        {
            gameObject.Link(entity, contexts.game);
        }
    }
}
