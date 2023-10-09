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
        /// 获得第一段跳跃，持续跳跃行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns></returns>
        public bool GetFirstJumpContinueInput()
        {
            return !isStopPlayerInput && Input.GetButton("Jump");
        }


        /// <summary>
        /// 获得突然跳跃行为的输入，二段跳，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public bool GetJumpButtonDownInput()
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
        /// 获得普通攻击行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns>返回一个Bool值</returns>
        public bool GetAttackInput()
        {
            return !isStopPlayerInput && Input.GetButtonDown("Attack");
        }

        /// <summary>
        /// 获得射箭攻击行为的输入，可以在ProjectSetting中修改按键
        /// </summary>
        /// <returns></returns>
        public bool GetBowAttackInput()
        {
            return !isStopPlayerInput && Input.GetButtonDown("BowAttack");
        }


        // /// <summary>
        // /// 获得第一段跳跃按键的回弹，每次按下之后，只能执行一次跳跃，必须松开按键再次进行
        // /// </summary>
        // /// <returns></returns>
        // public bool GetFirstJumpButtonUp()
        // {
        //     return !isStopPlayerInput && Input.GetButtonUp("Jump");
        // }

        // /// <summary>
        // /// 获得想要开始跑步行为的输入，可以在ProjectSetting中修改按键
        // /// </summary>
        // /// <returns>返回一个Bool值</returns>
        // public static bool GetStartRunInput()
        // {
        //     return Input.GetButton("Run");
        // }
    }
}