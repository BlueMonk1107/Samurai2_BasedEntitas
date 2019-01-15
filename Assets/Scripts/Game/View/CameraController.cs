using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Ïà»ú¿ØÖÆ
    /// </summary>
    public class CameraController : ViewService ,IGameCameraStateListener
    {
        public override void Init()
        {
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
            gameObject.Link(entity, Contexts.sharedInstance.game);
            entity.AddGameCameraStateListener(this);
        }

        public void OnGameCameraState(GameEntity entity, CameraAniName state)
        {
            throw new System.NotImplementedException();
        }
    }
}
