using ZFramework.Interfaces;
using ZFramework.Tools;

namespace ZFramework.Managers
{
    public class UIManager : MonoSingleton<UIManager>, IManager
    {
        public void RegisterIntoDict()
        {
            GameArchitecture.RegisterManager(this);
        }
    }
}