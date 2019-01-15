using UnityEngine;

namespace Game
{
    public class Services      
    {
        public IFindObjectService FindObjectService { get; private set; }

        public Services(IFindObjectService findObjectService)
        {
            FindObjectService = findObjectService;
        }
    }
}
