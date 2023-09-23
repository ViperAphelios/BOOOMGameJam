using System;
using UnityEngine;
using UnityEngine.Events;
using ZFramework.Interfaces;
using ZFramework.Tools;

namespace General
{
    public abstract class Character : MonoBehaviour, IModel
    {
        [Header("基本属性")]
        public float maxHealth;

        public float attackDamage;
        public float speed;

        [Header("当前属性值")]
        public float currentHealth;

        public float currentSpeed;

        [Header("基本状态")]
        public bool isOnGround;

        //受伤无敌时的状态
        public bool isInjuredInvincible;

        //受伤闪烁和位移时的状态
        public bool isHurt;

        public bool isDead;

        //处于攻击状态
        public bool isAttack;

        [Header("受伤计时器")]
        public float injuredDuration;

        public float injuredCurrentTime;

        [Header("基本事件")]
        public UnityEvent<Transform> onTakeDamage;

        public UnityEvent onDie;

        public UnityEvent<Character> onHealthChange;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
            currentSpeed = speed;
            injuredCurrentTime = injuredDuration;
        }

        protected virtual void Start()
        {
            onHealthChange?.Invoke(this);
        }

        /// <summary>
        ///     受伤计时器
        /// </summary>
        public void InjuredTimer()
        {
            if (!isInjuredInvincible) return;
            injuredCurrentTime -= Time.deltaTime;
            if (injuredCurrentTime <= 0f)
            {
                isInjuredInvincible = false;
                injuredCurrentTime = injuredDuration;
            }
        }

        /// <summary>
        ///     受伤方法
        /// </summary>
        /// <param name="obj"> 造成受伤的物体 </param>
        /// <param name="damage"> 基础伤害数值 </param>
        public void TakeDamage(GameObject obj, float damage)
        {
            //Debug.Log("攻击伤害是：" + attacker.damage);
            if (currentHealth >= damage)
            {
                currentHealth -= damage;
                //触发受伤事件，控制除了数据变换以外的方法
                onTakeDamage?.Invoke(obj.transform);
            }
            else
            {
                currentHealth = 0f;
                //触发死亡事件
                onDie?.Invoke();
            }

            //进入受伤无敌状态
            isInjuredInvincible = true;

            onHealthChange?.Invoke(this);
        }
    }
}