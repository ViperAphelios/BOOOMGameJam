using UnityEngine;
using ZFramework.Interfaces;
using ZFramework.Tools;

namespace ZFramework.Managers
{
    public class OldInputManager : MonoSingleton<OldInputManager>, IManager
    {
        public bool isStopPlayerInput;

        public void RegisterIntoDict()
        {
            GameArchitecture.RegisterManager(this);
        }

        /// <summary>
        /// 获得横向轴的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个横向的Vector2向量</returns>
        public Vector2 GetHorizontalMove()
        {
            return isStopPlayerInput ? Vector2.zero : new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        }

        /// <summary>
        /// 获得纵向轴的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个纵向的Vector2向量</returns>
        public Vector2 GetVerticalInput()
        {
            return isStopPlayerInput ? Vector2.zero : new Vector2(Input.GetAxisRaw("Vertical"), 0);
        }

        /// <summary>
        /// 获得跳跃行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public bool GetJumpInput()
        {
            return !isStopPlayerInput && Input.GetButtonDown("Jump");
        }

        /// <summary>
        /// 获得冲刺行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public bool GetDashInput()
        {
            return !isStopPlayerInput && Input.GetButtonDown("Dash");
        }

        /// <summary>
        /// 获得攻击行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public bool GetAttackInput()
        {
            return !isStopPlayerInput && Input.GetButtonDown("Attack");
        }

        /// <summary>
        /// 获得想要开始跑步行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public static bool GetStartRunInput()
        {
            return Input.GetButton("Run");
        }
    }
}