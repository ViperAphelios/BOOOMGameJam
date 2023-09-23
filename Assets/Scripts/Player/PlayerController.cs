using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.Events;
using ZFramework.Interfaces;
using ZFramework.Managers;

namespace Player
{
    public class PlayerController : MonoBehaviour, ICharacterController
    {
        public IController Controller => this;


        // 前向方向
        public Vector2 forwardDirection;

        // Input的值
        public Vector2 inputHorizontalValue;

        private PlayerAnimation mPlayerAnimation;
        private Rigidbody2D mRb;
        private PlayerModel mModel;

        // Jump的委托事件
        private UnityAction mOnJump;

        [Header("传感器")]
        public RaySensor2D onGroundSensor;

        // 和该脚本在同一个物体上的类引用
        private void Awake()
        {
            mModel = Controller.GetModel<PlayerModel>(gameObject);
            mRb = GetComponent<Rigidbody2D>();
        }

        // 位于该脚本的子物体或者父物体的引用
        private void Start()
        {
            InitAction();
            mPlayerAnimation = GetComponentInChildren<PlayerAnimation>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        // 顺序-采集按键-修正方向
        private void Update()
        {
            InputCheck();
            CorrectPlayerDirection();
            SensorPulseAndCheck();
        }

        private void OnDestroy()
        {
            CancelAction();
        }

        public void InitManager()
        {
            throw new System.NotImplementedException();
        }

        public void InitAction()
        {
            mOnJump += Jump;
        }

        public void CancelAction()
        {
            mOnJump -= Jump;
        }

        /// <summary>
        /// 传感器统一发射脉冲和检测结果
        /// </summary>
        private void SensorPulseAndCheck()
        {
            // 角色是否在地面上
            onGroundSensor.Pulse();
            mModel.isOnGround = onGroundSensor.GetNearestDetection();
        }

        /// <summary>
        /// 修正玩家的方向，包括角色素材朝向和前向方向
        /// </summary>
        private void CorrectPlayerDirection()
        {
            if (inputHorizontalValue.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1, 1);
            }

            if (inputHorizontalValue.x > 0)
            {
                transform.localScale = new Vector3(1f, 1, 1);
            }

            forwardDirection = new Vector2(transform.localScale.x, 0);
        }

        private void Move()
        {
            mRb.velocity = new Vector2(inputHorizontalValue.x * mModel.currentSpeed * Time.fixedDeltaTime,
                mRb.velocity.y);
        }

        private void Jump()
        {
            mRb.velocity = new Vector2(mRb.velocity.x, 0);
            mRb.AddForce(Vector2.up * mModel.jumpForce, ForceMode2D.Impulse);
            mModel.remainingJumpNum -= 1;

            // Debug.Log("跳跃一次");
        }

        /// <summary>
        /// 玩家按键输入，优先使用OldInputManager单例类封装
        /// </summary>
        private void InputCheck()
        {
            // 横向移动输入
            inputHorizontalValue = OldInputManager.GetHorizontalMove();
            mModel.isWalk = Mathf.Abs(inputHorizontalValue.x) >= 0.3f;

            // 跳跃输入
            if (OldInputManager.GetJumpInput())
            {
                // 满足第一段跳跃或者第二段跳跃的条件即可发布跳跃委托
                if ((mModel.isJump && mModel.remainingJumpNum > 0) || (mModel.isOnGround && !mModel.isJump))
                {
                    mModel.isJump = true;
                    mOnJump?.Invoke();
                }
            }
        }

    #region UnityEvent|Inspector面板挂载的方法

        // 玩家着陆到地面，RaySensor物体触发
        public void PlayerLandingOnGround(GameObject obj, Sensor sensor)
        {
            if (mModel == null) return;
            mModel.isJump = false;
            mModel.remainingJumpNum = mModel.maxExtraJumpNum;
        }

    #endregion
    }
}