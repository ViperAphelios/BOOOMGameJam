using UnityEngine;
using ZFramework.Interfaces;

namespace Player
{
    public class PlayerSpriteAnimEventControl : MonoBehaviour, ICharacterController
    {
        public IController Controller => this;

        private PlayerModel mModel;

        // Start is called before the first frame update
        void Start()
        {
            mModel = GetComponentInParent<PlayerModel>();
        }

        // Update is called once per frame
        void Update()
        { }


        public void InitManager()
        {
            throw new System.NotImplementedException();
        }

        public void InitAction()
        {
            throw new System.NotImplementedException();
        }

        public void CancelAction()
        {
            throw new System.NotImplementedException();
        }

    #region AnimationEvent 动画事件

        // 结束攻击状态                                                                                                                                                                                                                                                                                                                                                                                                                             
        public void EndAttackState()
        {
            mModel.isAttack = false;
        }

    #endregion
    }
}