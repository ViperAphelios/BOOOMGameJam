using System;
using UnityEngine;
using ZFramework.Interfaces;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour, IController
    {
        public IController Controller => this;

        private PlayerModel mModel;
        private Animator mAnimator;
        private Rigidbody2D mRb;

        // 可以提高性能，不用遍历所有字符串对比
        private static readonly int IsMove = Animator.StringToHash("isMove");
        private static readonly int IsJump = Animator.StringToHash("isJump");
        private static readonly int IsDash = Animator.StringToHash("isDash");
        private static readonly int IsAttack = Animator.StringToHash("isAttack");
        private static readonly int VelocityY = Animator.StringToHash("velocityY");
        private static readonly int VelocityX = Animator.StringToHash("velocityX");

        private void Awake()
        {
            mModel = Controller.GetModel<PlayerModel>(gameObject);
            mRb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            mAnimator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            SetAnimationValue();
        }

        /// <summary>
        /// 设置Animator控制器的参数
        /// </summary>
        private void SetAnimationValue()
        {
            mAnimator.SetBool(IsMove, mModel.isMove);
            mAnimator.SetBool(IsJump, mModel.isJump);
            mAnimator.SetBool(IsDash, mModel.isDash);
            mAnimator.SetBool(IsAttack, mModel.isAttack);
            mAnimator.SetFloat(VelocityY, mRb.velocity.y);
            mAnimator.SetFloat(VelocityX, Mathf.Abs(mRb.velocity.x));
        }

        // 播放着陆动画
        public void PlayLandAnimation()
        {
            mAnimator.Play("Land");
        }

        // 播放向前攻击动画
        public void PlayForwardAttackAnim()
        {
            mAnimator.Play("Attack");
        }

        // 播放向上攻击动画
        public void PlayUpAttackAnim()
        {
            mAnimator.Play("AttackUp");
        }

        // 播放向下攻击动画
        public void PlayDownAttackAnim()
        {
            mAnimator.Play("AttackDown");
        }
    }
}