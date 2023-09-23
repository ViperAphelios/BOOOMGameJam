using Micosmo.SensorToolkit;
using UnityEngine;

namespace ZFramework.Tools
{
    /// <summary>
    ///     Sensor的常用方法集合包
    /// </summary>
    public static class SensorMethodPack
    {
        /// <summary>
        ///     获得Sensor检测到的最近的物体
        /// </summary>
        /// <param name="sensor">要执行该方法的Sensor检查器</param>
        /// <returns></returns>
        public static GameObject GetNearestGameObject(Sensor sensor)
        {
            return sensor.GetNearestDetection();
        }
    }
}