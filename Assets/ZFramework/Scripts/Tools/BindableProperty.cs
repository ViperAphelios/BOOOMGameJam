using System;
using UnityEngine.Events;

namespace ZFramework.Tools
{
    /// <summary>
    /// 绑定属性，一个可以比较的类，自带一个数据变化时候调用的委托，用于充当数据变量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableProperty<T> where T : IEquatable<T>
    { 
        UnityAction<T> mOnValueChanged = e => { };
        T mValue;

        public T Value
        {
            get => mValue;
            set
            {
                if (mValue.Equals(value)) return;
                mValue = value;
                mOnValueChanged?.Invoke(value);
            }
        }
    }
}