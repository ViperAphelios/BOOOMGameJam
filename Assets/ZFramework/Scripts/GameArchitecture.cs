using System;
using System.Collections.Generic;
using UnityEngine;
using ZFramework.Interfaces;
using ZFramework.Managers;
using ZFramework.Tools;

namespace ZFramework
{
    public class GameArchitecture : MonoSingleton<GameArchitecture>
    {
        public static Dictionary<Type, IManager> allManagersDict = new();

        public GameObject managersGameObject;


        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            CreateManagersGameObject();
            MakeSureEasySaveManager();
            LoadManagers();
        }

        private void Start()
        { }

        /// <summary>
        ///     输出检查当前的字典
        /// </summary>
        private void CheckDict()
        {
            foreach (var keyValue in allManagersDict) Debug.Log("Key: " + keyValue.Key + " Value: " + keyValue.Value);
        }

        private void CreateManagersGameObject()
        {
            var obj = new GameObject
            {
                name = "//Managers"
            };
            managersGameObject = obj;
        }

        private void MakeSureEasySaveManager()
        {
            var es3Manager = Resources.Load("Easy Save 3/Easy Save 3 Manager");
            if (!FindObjectOfType<ES3ReferenceMgr>())
            {
                var obj = Instantiate(es3Manager, managersGameObject.transform);
                obj.name = "Easy Save 3 Manager";
            }
        }

        void LoadManagers()
        {
            CreateManager<OldInputManager>();
        }

    #region Manager 架构私有方法

        /// <summary>
        ///     创造新的Manager，通常位于架构的Awake
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void CreateManager<T>() where T : MonoSingleton<T>, IManager
        {
            if (FindObjectOfType<T>()) return;
            var obj = new GameObject();
            var type = typeof(T).Name;
            obj.name = type;
            obj.transform.SetParent(managersGameObject.transform);
            obj.AddComponent<T>();
            obj.GetComponent<T>().RegisterIntoDict();
        }

    #endregion

    #region Manager 架构公开方法

        public static void RegisterManager<T>(T obj) where T : class, IManager
        {
            var type = typeof(T);
            allManagersDict[type] = obj;
        }

        /// <summary>
        ///     从管理器字典里面，获得需要的Manager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetManager<T>() where T : class, IManager
        {
            var type = typeof(T);
            if (allManagersDict.TryGetValue(type, out var value)) return value as T;

            return null;
        }

    #endregion
    }
}