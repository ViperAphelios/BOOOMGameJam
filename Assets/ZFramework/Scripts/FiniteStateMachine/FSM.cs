using System.Collections.Generic;
using UnityEngine;
using ZFramework.Interfaces;

namespace General.FSM
{
    /// <summary>
    ///     项目所有AI状态的枚举类，
    ///     该枚举类为案例模板，建议在新项目中注释掉这个，另写一个新的全局枚举类
    /// </summary>
    public enum StateType
    {
        Patrol,
        Chase,
        Attack,
        Die
    }

    /// <summary>
    ///     抽象基础状态类，新的状态类继承自该类
    /// </summary>
    public abstract class AbstractState
    {
        public Fsm ownerFsm;

        //以下是继承该抽象类的具体状态类对象的构造函数模板，每个状态类对象都要有一个自己的构造函数，复制后修改
        //- 它是哪个FSM管理的
        //- 它是和哪个数据交流的
        //- 它是和哪个角色控制器交互的
        // public ConcreteState(Fsm fsm, BoarModel model,ICharacterController characterController)
        // {
        //     ownerFsm = fsm;
        //     mBoarModel = model;
        //     myController = characterController;
        // }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
        public abstract void OnFixedUpdate();
        public abstract void OnCheck();
    }

    public class Fsm
    {
        public ICharacterController characterController;
        public AbstractState currentState;
        public Dictionary<StateType, AbstractState> statesDict = new();

        /// <summary>
        ///     直接设置AI的初始状态方法，不执行OnEnter
        /// </summary>
        /// <param name="stateType"></param>
        public void SetInitialState(StateType stateType)
        {
            currentState = statesDict[stateType];
        }

        /// <summary>
        ///     添加新的状态进入AI的可用状态字典
        /// </summary>
        /// <param name="stateType">可用状态字典的键</param>
        /// <param name="state">具体状态类对象</param>
        public void AddState(StateType stateType, AbstractState state)
        {
            if (statesDict.ContainsKey(stateType))
            {
                Debug.Log("[AddState] >>>>>>>>> has contain key" + stateType);
            }

            statesDict.Add(stateType, state);
        }

        /// <summary>
        ///     AI切换状态的方法，会执行上一个状态的退出OnExit和下一个状态的进入OnEnter
        /// </summary>
        /// <param name="stateType"></param>
        public void SwitchState(StateType stateType)
        {
            if (!statesDict.ContainsKey(stateType))
            {
                Debug.Log("[SwitchState] >>>>>>>>> not contain key" + stateType);
            }

            currentState?.OnExit();
            currentState = statesDict[stateType];
            currentState.OnEnter();
        }

        public void OnUpdate()
        {
            currentState.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            currentState.OnFixedUpdate();
        }

        public void OnCheck()
        {
            currentState.OnCheck();
        }
    }
}