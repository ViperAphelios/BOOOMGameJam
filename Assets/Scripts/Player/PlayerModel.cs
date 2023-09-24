using General;
using UnityEngine;

namespace Player
{
    public class PlayerModel : Character
    {
        [Header("跳跃和奔跑")]
        // 跳跃相关
        public float jumpForce;

        public int maxExtraJumpNum;
        public int remainingJumpNum;
        public bool canSecondJump;

        // 跑步
        public float runSpeed;

        [Header("冲刺相关")]
        // 冲刺(闪避)
        public float dashSpeed;

        public float dashTimeSecond;
        public float dashCoolDown;
        public bool canDash;
        public bool dashCanInvincible;
        public bool isDashInvincible;

        [Header("土狼时间")]
        // 土狼时间,帧
        public int maxCoyoteTimeFrame;

        public int currentCoyoteTimeFrame;


        [Header("角色额外状态")]
        public bool isClimbUp;

        public bool isWalk;
        public bool isRun;
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

            // 跑步参数默认
            if (runSpeed <= 0)
            {
                runSpeed = 1.5f * normalSpeed;
            }

            // 土狼时间参数默认
            if (maxCoyoteTimeFrame <= 0)
            {
                maxCoyoteTimeFrame = 3;
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