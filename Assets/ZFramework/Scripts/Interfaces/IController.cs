using UnityEngine;

namespace ZFramework.Interfaces
{
    /// <summary>
    /// 最基础的控制器接口，通常用于单个类只需要获得Model的情况
    /// </summary>
    public interface IController
    {
        public IController Controller { get; }
        
        /// <summary>
        ///     MVC模式，Controller需要获得Model
        /// </summary>
        public T GetModel<T>(GameObject obj)
        {
            return obj.GetComponent<T>();
        }
    }
}