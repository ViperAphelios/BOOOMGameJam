namespace ZFramework.Interfaces
{
    /// <summary>
    /// 通常用于具体角色（包括玩家和AI）控制器的接口
    /// </summary>
    public interface ICharacterController : IController
    {
        /// <summary>
        /// 选择需要调用的Manager，进行初始化赋值
        /// </summary>
        void InitManager();

        /// <summary>
        /// 初始化订阅委托事件等
        /// </summary>
        void InitAction();
        
        /// <summary>
        /// 取消当前订阅的事件，手动取消不会触发GC，可以提高性能，也不会出错
        /// </summary>
        void CancelAction();
    }
}