using UnityEngine;

namespace Game
{
    public class InitServiceFeature : Feature     
    {
        public InitServiceFeature(Contexts contexts, Services services) : base("InitService")
        {
            contexts.game.ReplaceGameFindObjectService(services.FindObjectService);
        }
    }
}
