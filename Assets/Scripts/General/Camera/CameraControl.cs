using System;
using Cinemachine;
using UnityEngine;

namespace General
{
    public class CameraControl : MonoBehaviour
    {
        private CinemachineConfiner2D mConfiner2D;

        private void Awake()
        {
            mConfiner2D = GetComponent<CinemachineConfiner2D>();
        }

        private void Start()
        {
            SetNewCameraBounds();
        }

        /// <summary>
        /// 设置新的虚拟摄像机边界
        /// </summary>
        private void SetNewCameraBounds()
        {
            var obj = GameObject.FindWithTag("Bounds");
            if (obj == null)
                return;

            // 所有碰撞体都是Collider2D的子类
            mConfiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();

            // 获得新的图形要强制刷新缓存
            mConfiner2D.InvalidateCache();
        }
    }
}