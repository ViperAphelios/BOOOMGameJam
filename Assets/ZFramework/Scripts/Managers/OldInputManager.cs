using UnityEngine;
using ZFramework.Interfaces;
using ZFramework.Tools;

namespace ZFramework.Managers
{
    public class OldInputManager : MonoSingleton<OldInputManager>, IManager
    {
        public void RegisterIntoDict()
        {
            GameArchitecture.RegisterManager(this);
        }

        /// <summary>
        /// 获得横向轴的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个横向的Vector2向量</returns>
        public static Vector2 GetHorizontalMove()
        {
            return new Vector2(Input.GetAxis("Horizontal"), 0);
        }
        
        /// <summary>
        /// 获得想要开始跑步行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns></returns>
        public bool GetStartRunInput()
        {
            return Input.GetButton("Run");
        }

        /// <summary>
        /// 获得跳跃行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public static bool GetJumpInput()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}