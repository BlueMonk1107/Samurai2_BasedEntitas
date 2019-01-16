using UnityEngine;

namespace Game
{
    public class Services      
    {
        public IFindObjectService FindObjectService { get; private set; }
        public IInputService EntitasInputService { get; private set; }
        public IInputService UnityInputService { get; private set; }
        public ILogService LogService { get; private set; }

        public Services(IFindObjectService findObjectService, IInputService entitasInputService, IInputService unityInputService, ILogService logService)
        {
            FindObjectService = findObjectService;
            EntitasInputService = entitasInputService;
            UnityInputService = unityInputService;
            LogService = logService;
        }
    }
}
