using General;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerModel : Character
    {
        [Header("跳跃相关")]
        public float jumpForce;

        public int maxExtraJumpNum;
        public int remainingJumpNum;
        public bool canSecondJump;

        // 跑步 , 移除跑步
        // public float runSpeed;

        [Header("冲刺相关")]
        public float dashSpeed;

        public float dashTimeSecond;
        public float dashCoolDown;
        public bool canDash;
        public bool dashCanInvincible;
        public bool isDashInvincible;

        [Header("土狼时间")]
        // 土狼时间,秒
        public float maxCoyoteTime;

        public bool isCoyote;
        //public float currentCoyoteTimeFrame;


        [Header("角色额外状态")]
        public bool isClimbUp;

        public bool isMove;
        public bool isJump;
        public bool isDash;

        protected override void Start()
        {
            base.Start();

            // 跳跃参数默认
            if (maxExtraJumpNum <= 0)
            {
                maxExtraJumpNum = 2;
            }

            remainingJumpNum = maxExtraJumpNum;

            // 土狼时间参数默认
            if (maxCoyoteTime <= 0)
            {
                maxCoyoteTime = 0.15f;
            }

            // 冲刺相关参数默认
            if (dashTimeSecond <= 0)
            {
                dashTimeSecond = 0.15f;
            }

            if (dashCoolDown <= 0)
            {
                dashCoolDown = 2f;
            }

            canDash = true;
        }
    }
}