using General;
using UnityEngine;

namespace Player
{
    public class PlayerModel : Character
    {
        [Header("跳跃相关")]
        // 单次，首次跳跃的瞬时力的常量
        public float singleJumpForce;

        // 持续跳跃的力的常量
        public float firstJumpContinueForce;

        public bool isFirstJumpDown;
        public bool isFirstJumpUp;
        public bool isFirstJumpStopTime;
        public float firstJumpForce;
        public bool isSecondJump;
        public float secondJumpForce;
        public bool canSecondJump;

        [Header("起步加速和静止减速时间")]
        [Range(0, 0.5f)]
        public float startAccelerationTime;

        [Range(0, 0.3f)]
        public float endDecelerateTime;

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

            // 加速时间和减速时间参数默认
            if (startAccelerationTime <= 0)
            {
                startAccelerationTime = 0.12f;
            }

            if (endDecelerateTime <= 0)
            {
                endDecelerateTime = 0.06f;
            }

            // 首次跳跃的力的参数默认
            if (singleJumpForce <= 0)
            {
                singleJumpForce = 680f;
            }

            if (firstJumpContinueForce <= 0)
            {
                firstJumpContinueForce = 460f;
            }
        }
    }
}