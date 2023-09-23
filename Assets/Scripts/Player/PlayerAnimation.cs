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
        private static readonly int IsWalk = Animator.StringToHash("isWalk");

        private void Awake()
        {
            mModel = Controller.GetModel<PlayerModel>(gameObject);
        }

        private void Start()
        {
            mAnimator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            SetAnimationValue();
        }

        private void SetAnimationValue()
        {
            mAnimator.SetBool(IsWalk, mModel.isWalk);
        }
    }
}