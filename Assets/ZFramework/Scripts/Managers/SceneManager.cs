using ZFramework.Interfaces;
using ZFramework.Tools;

namespace ZFramework.Managers
{
    public class SceneManager : MonoSingleton<SceneManager>, IManager
    {
        public void RegisterIntoDict()
        {
            GameArchitecture.RegisterManager(this);
        }
    }
}