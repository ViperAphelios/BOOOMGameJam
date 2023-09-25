using Micosmo.SensorToolkit;
using Timers;
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

        // Input轴向输入的值
        public Vector2 inputHorizontalValue;
        public Vector2 inputVerticalValue;


        [Header("起步加速和结束减速")]
        public bool hasAccelerationAndDecelerate;

        public bool isDebugAccelerationAndDecelerate;
        public bool startAcceleration;
        public bool endDecelerate;

        [Header("是否有跳跃缓存")]
        public bool haveJumpCache;

        [Header("是否处于冲刺静止阶段")]
        public bool isDashStationary;

        [Header("传感器")]
        public RaySensor2D onGroundSensor;

        public RaySensor2D cacheJumpSensor;
        public RangeSensor2D coyoteTimeSensor;

        private Rigidbody2D mRb;
        private PlayerModel mModel;

        // Jump的委托，Update检测按键输入
        private UnityAction mOnJump;


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
        }

        private void FixedUpdate()
        {
            // 如果在冲刺静止阶段，直接退出FixedUpdate
            if (isDashStationary)
            {
                mRb.velocity = Vector2.zero;
                return;
            }

            if (mModel.isAttack)
            {
                mRb.velocity = new Vector2(0, mRb.velocity.y);
                return;
            }

            // 非冲刺状态才执行普通移动
            if (!mModel.isDash)
            {
                Move();
            }
            else
            {
                if (!mModel.canDash)
                {
                    Dash();
                }
            }


            LimitMaxVelocityY();
        }


        // 顺序-土狼时间计时-采集按键-修正方向-脉冲检测
        private void Update()
        {
            PreventMoveErrorJam();
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
        /// 传感器统一发射脉冲和检测结果,每帧进行的传感器
        /// </summary>
        private void SensorPulseAndCheck()
        {
            // 角色是否在地面上
            onGroundSensor.Pulse();
            mModel.isOnGround = onGroundSensor.GetNearestDetection();

            // 如果有缓存直接触发
            if (haveJumpCache && mModel.isOnGround)
            {
                mOnJump?.Invoke();
            }
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
            if (hasAccelerationAndDecelerate)
            {
                // 起步加速和结束减速检测
                EndMoveCheck();
                StartMoveCheck();
            }

            // 调整速度
            if (mModel.isMove)
            {
                mModel.currentSpeed = mModel.normalSpeed;
            }

            mRb.velocity = new Vector2(inputHorizontalValue.x * mModel.currentSpeed * Time.fixedDeltaTime,
                mRb.velocity.y);
        }

        // 冲刺(闪避)
        private void Dash()
        {
            mRb.velocity = new Vector2(forwardDirection.x * mModel.dashSpeed * Time.fixedDeltaTime, 0);
        }

        private void Jump()
        {
            // 如果在冲刺静止阶段，直接退出Jump
            if (isDashStationary)
            {
                return;
            }

            haveJumpCache = false;
            mModel.isJump = true;
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
            // 如果移动加速和停止减速
            if (hasAccelerationAndDecelerate)
            {
                if (inputHorizontalValue != new Vector2(0, 0) &&
                    Mathf.Abs(OldInputManager.Instance.GetHorizontalMove().x) < 1)
                {
                    endDecelerate = true;
                    // 防卡死机制，这个减速状态最多保持0.51s
                    TimersManager.SetTimer(this, 0.51f, () => { endDecelerate = false; });
                }

                if (inputHorizontalValue == new Vector2(0, 0) &&
                    Mathf.Abs(OldInputManager.Instance.GetHorizontalMove().x) > 0
                    && !endDecelerate)
                {
                    startAcceleration = true;
                    // 防卡死机制，这个加速状态最多保持0.51s
                    TimersManager.SetTimer(this, 0.51f, () => { startAcceleration = false; });
                    inputHorizontalValue = OldInputManager.Instance.GetHorizontalMove() * 1 /
                                           (mModel.startAccelerationTime * 50f);
                }
            }
            // 不进行移动加速和停止减速
            else
            {
                inputHorizontalValue = OldInputManager.Instance.GetHorizontalMove();
            }

            mModel.isMove = Mathf.Abs(inputHorizontalValue.x) >= 0.1f;

            // 跳跃输入
            if (OldInputManager.Instance.GetJumpInput())
            {
                cacheJumpSensor.Pulse();

                // 检查是否有跳跃缓存,按跳跃的时候才检查一次
                if (cacheJumpSensor.GetNearestDetection())
                {
                    haveJumpCache = true;
                }

                // 满足第一段跳跃或者第二段跳跃的条件即可发布跳跃委托
                if ((mModel.isJump && mModel.remainingJumpNum > 0 && mModel.canSecondJump && !haveJumpCache) ||
                    (mModel.isOnGround && !mModel.isJump))
                {
                    mOnJump?.Invoke();
                }

                // 土狼时间，既不在地面，又不在跳跃状态，
                if (mModel.isCoyote && !mModel.isJump && !mModel.isOnGround)
                {
                    mOnJump?.Invoke();
                }
            }

            // 冲刺输入
            if (OldInputManager.Instance.GetDashInput() && !mModel.isDash && mModel.canDash)
            {
                mModel.isDash = true;
                mModel.canDash = false;

                TryDash();
            }

            if (OldInputManager.Instance.GetAttackInput() && !mModel.isDash && !mModel.isAttack)
            {
                TryAttack();
            }
        }

        // 尝试攻击，攻击的触发逻辑
        private void TryAttack()
        {
            mModel.isAttack = true;
            inputVerticalValue = OldInputManager.Instance.GetVerticalInput();
            if (inputVerticalValue != Vector2.zero)
            {
                // TODO: 将执行不同方向的特殊攻击 
            }
            else
            {
                // TODO: 执行攻击
                Attack();
            }
        }

        private void Attack()
        {
            Debug.Log("攻击一次");
            mModel.isAttack = false;
        }

        /// <summary>
        /// 尝试冲刺，冲刺的触发逻辑，实际冲刺效果在FixedUpdate中的Dash()
        /// </summary>
        private void TryDash()
        {
            isDashStationary = true;
            // 如果解锁了冲刺无敌
            if (mModel.dashCanInvincible)
            {
                StartDashInvincible();
            }

            // 停止0.06s之后，进行冲刺计时，才进行冲刺操作
            TimersManager.SetTimer(this, 0.06f, () =>
            {
                isDashStationary = false;
                // 0.15秒后停止dash
                TimersManager.SetTimer(this, 0.15f, () =>
                {
                    mModel.isDash = false;
                    EndDashInvincible();

                    Debug.Log("结束冲刺,结束冲刺无敌");
                }, false, false);
                // 2秒后才可以再次dash
                TimersManager.SetTimer(this, mModel.dashCoolDown, () =>
                {
                    mModel.canDash = true;

                    Debug.Log("可以再次冲刺");
                }, false, false);
            }, false, false);
        }

        /// <summary>
        /// 起步加速
        /// </summary>
        private void StartMoveCheck()
        {
            // 检查是否在起步阶段
            if (!startAcceleration || !(Mathf.Abs(inputHorizontalValue.x) < 1)) return;
            if (inputHorizontalValue.x < 0)
            {
                inputHorizontalValue += new Vector2(-1f / (mModel.startAccelerationTime * 50f), 0);
            }

            if (inputHorizontalValue.x > 0)
            {
                inputHorizontalValue += new Vector2(1f / (mModel.startAccelerationTime * 50f), 0);
            }

            // 限制最大值
            if (inputHorizontalValue.x <= -1)
            {
                startAcceleration = false;
                inputHorizontalValue = new Vector2(-1, 0);
            }

            if (inputHorizontalValue.x >= 1)
            {
                startAcceleration = false;
                inputHorizontalValue = new Vector2(1, 0);
            }

            if (isDebugAccelerationAndDecelerate)
            {
                Debug.Log("加速阶段该帧水平横向速度为：" + inputHorizontalValue.x * mModel.currentSpeed * Time.fixedDeltaTime);
            }
        }

        /// <summary>
        /// 结束减速
        /// </summary>
        private void EndMoveCheck()
        {
            // 检查是否在减速阶段
            if (!endDecelerate) return;
            if (inputHorizontalValue.x < 0)
            {
                inputHorizontalValue += new Vector2(1f / (mModel.endDecelerateTime * 50f), 0);
                if (isDebugAccelerationAndDecelerate)
                {
                    Debug.Log("减速阶段该帧水平横向速度为：" + inputHorizontalValue.x * mModel.currentSpeed * Time.fixedDeltaTime);
                }

                if (inputHorizontalValue.x > 0)
                {
                    endDecelerate = false;
                    inputHorizontalValue = new Vector2(0, 0);
                }
            }

            if (inputHorizontalValue.x > 0)
            {
                inputHorizontalValue += new Vector2(-1f / (mModel.endDecelerateTime * 50f), 0);
                if (isDebugAccelerationAndDecelerate)
                {
                    Debug.Log("减速阶段该帧水平横向速度为：" + inputHorizontalValue.x * mModel.currentSpeed * Time.fixedDeltaTime);
                }

                if (inputHorizontalValue.x < 0)
                {
                    endDecelerate = false;
                    inputHorizontalValue = new Vector2(0, 0);
                }
            }
        }

        /// <summary>
        /// 开始冲刺无敌
        /// </summary>
        private void StartDashInvincible()
        {
            mModel.isDashInvincible = true;
            // Todo: 冲刺无敌的操作
        }

        /// <summary>
        /// 结束冲刺无敌
        /// </summary>
        private void EndDashInvincible()
        {
            mModel.isDashInvincible = false;
            // TODO: 结束无敌状态，恢复正常
        }

        /// <summary>
        /// 限制最大下落速度
        /// </summary>
        private void LimitMaxVelocityY()
        {
            var velocity = mRb.velocity;
            mRb.velocity = new Vector2(velocity.x, Mathf.Max(velocity.y, -15f));
        }

        /// <summary>
        /// 防止有人用极快的速度连续左右横跳，导致角色加速的同时又减速，直接卡住
        /// </summary>
        private void PreventMoveErrorJam()
        {
            if (endDecelerate && startAcceleration)
            {
                endDecelerate = false;
                startAcceleration = false;
                mRb.velocity = new Vector2(0f, mRb.velocity.y);
            }
        }

    #region UnityEvent|Inspector面板挂载的方法

        // 玩家着陆到地面，RaySensor物体触发
        public void PlayerLandingOnGround(GameObject obj, Sensor sensor)
        {
            if (mModel == null)
            {
                Debug.Log("没有Model");
                return;
            }

            // 刷新可跳跃次数
            mModel.isJump = false;
            mModel.remainingJumpNum = mModel.maxExtraJumpNum;
        }

        // 玩家离开地面的一瞬间检测是否属于土狼时间
        public void PlayerIsCoyoteTime(GameObject obj, Sensor sensor)
        {
            if (mModel == null)
            {
                Debug.Log("没有Model");
                return;
            }

            // 触发一次土狼时间传感器
            coyoteTimeSensor.transform.localPosition = new Vector3(-0.3f, 0, 0);
            coyoteTimeSensor.Pulse();
            mModel.isCoyote = coyoteTimeSensor.GetNearestDetection();
            if (mModel.isCoyote)
            {
                // 开始土狼时间计时,时间结束后触发土狼时间状态为假
                TimersManager.SetTimer(this, mModel.maxCoyoteTime, () =>
                {
                    mModel.isCoyote = false;
                    coyoteTimeSensor.transform.localPosition = new Vector3(0f, 0.5f, 0);
                    coyoteTimeSensor.Pulse();
                });
            }
        }

    #endregion
    }
}