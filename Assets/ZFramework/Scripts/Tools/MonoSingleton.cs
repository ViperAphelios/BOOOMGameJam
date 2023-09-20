using UnityEngine;

namespace ZFramework.Tools
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        // 静态 私有 泛型 变量

        public static T Instance { get; private set; }

        //检查当前的泛型单例模式是否已经生成  
        public static bool IsInitialized => Instance != null;

        protected virtual void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = (T)this;
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}