using System;
using System.Reflection;

namespace ZFramework.Tools
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //找到所有非Public的构造方法
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    //从ctors数组中获取无参的构造方法
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null) throw new Exception("Non-public ctor() not found");

                    instance = ctor.Invoke(null) as T;
                }

                return instance;
            }
        }


        protected virtual void OnDestroy()
        {
            if (instance == this) instance = null;
        }
    }
}