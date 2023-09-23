using General;
using UnityEngine;

namespace Player
{
    public class PlayerModel : Character
    {
        [Header("角色额外属性")]
        // 跳跃相关
        public float jumpForce;

        public int maxExtraJumpNum;
        public int remainingJumpNum;

        [Header("角色额外状态")]
        public bool isClimbUp;

        public bool isWalk;

        public bool isJump;

        protected override void Start()
        {
            base.Start();
            if (maxExtraJumpNum == 0)
            {
                maxExtraJumpNum = 2;
            }

            remainingJumpNum = maxExtraJumpNum;
        }
    }
}