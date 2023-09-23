using UnityEngine;
using ZFramework.Interfaces;

namespace ZFramework.Managers
{
    public class OldInputManager : MonoBehaviour ,IManager
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void RegisterIntoDict()
        {
            GameArchitecture.RegisterManager(this);
        }
    }
}
