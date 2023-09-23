using UnityEngine;
using ZFramework.Interfaces;
using ZFramework.Tools;

namespace ZFramework.Managers
{
    public class OldInputManager : MonoSingleton<OldInputManager>, IManager
    {
        /// <summary>
        /// 获得横向轴的输入
        /// </summary>
        /// <returns>返回一个横向的Vector2向量</returns>
        public Vector2 GetHorizontalMove()
        {
            return new Vector2(Input.GetAxis("Horizontal"), 0);
        }

        public void RegisterIntoDict()
        {
            GameArchitecture.RegisterManager(this);
        }
    }
}