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

        private UnityAction mOnMove;

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
        }

        private void OnDisable()
        {
            CancelAction();
        }

        public void InitManager()
        {
            throw new System.NotImplementedException();
        }

        public void InitAction()
        { }

        public void CancelAction()
        { }

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

        /// <summary>
        /// 玩家按键输入，优先使用OldInputManager单例类封装
        /// </summary>
        private void InputCheck()
        {
            inputHorizontalValue = OldInputManager.Instance.GetHorizontalMove();
            mModel.isWalk = Mathf.Abs(inputHorizontalValue.x) >= 0.3f;
        }
    }
}