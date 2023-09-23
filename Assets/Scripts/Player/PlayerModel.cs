using General;
using UnityEngine;

namespace Player
{
    public class PlayerModel : Character
    {
        [Header("角色额外属性")]
        public float currentSpeed;

        public float jumpForce;

        [Header("角色额外状态")]
        public bool isClimbUp;

        public bool isWalk;

        public bool isJump;

        protected override void Start()
        {
            base.Start();
            currentSpeed = speed;
        }
    }
}