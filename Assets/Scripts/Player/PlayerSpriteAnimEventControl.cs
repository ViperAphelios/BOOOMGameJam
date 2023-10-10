using Player.Arrow;
using UnityEngine;
using UnityEngine.Events;
using ZFramework.Interfaces;

namespace Player
{
    public class PlayerSpriteAnimEventControl : MonoBehaviour
    {
        // 玩家数据
        private PlayerModel mModel;

        // 开始射箭委托
        public UnityAction startArrowAction;

        // Start is called before the first frame update
        void Start()
        {
            mModel = GetComponentInParent<PlayerModel>();
        }


    #region AnimationEvent 动画事件

        // 结束攻击状态                                                                                                                                                                                                                                                                                                                                                                                                                             
        public void EndAttackState()
        {
            mModel.isAttack = false;
        }

        // 产生弓箭
        public void StartArrowAttack()
        {
            startArrowAction?.Invoke();
        }

    #endregion
    }
}