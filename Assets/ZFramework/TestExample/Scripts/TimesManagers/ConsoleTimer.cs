using System;
using Timers;
using UnityEngine;

namespace ZFramework.TestExample
{
    public class ConsoleTimer : MonoBehaviour
    {
        Timer mPrintTimer;
        Timer mDebugTimer;

        void Timer1()
        {
            Debug.Log("Test");
        }

        void ClearTimer1()
        {
            // Remove Timer1
            TimersManager.ClearTimer(Timer1);
        }

        void ForgottenTimer()
        { }

        void Awake()
        {
            //SetTimer指开始计时，三个不同的方法可以同时起效果
            //A SetTimer is a method of starting time. Three different methods can work at the same time
            // TimersManager.SetTimer(this, 3f, Timer.INFINITE_LOOPS, TestPrint1);
            // TimersManager.SetTimer(this, 5f, Timer.INFINITE_LOOPS, TestPrint2);
            // TimersManager.SetTimer(this, 10f, Timer.INFINITE_LOOPS, TestPrint3);

            //如果对同一个方法，同时进行多个计时，直接这样写，也可以执行
            //If multiple timings are performed on the same method at the same time,
            //it can also be executed by writing it in this way
            // TimersManager.SetTimer(this, 3f, Timer.INFINITE_LOOPS, TestPrintTime);
            // TimersManager.SetTimer(this, 5f, Timer.INFINITE_LOOPS, TestPrintTime);
            // TimersManager.SetTimer(this, 7f, Timer.INFINITE_LOOPS, TestPrintTime);

            //还是可以执行，只执行一次，分别是3，5，7秒的时候
            //It can still be executed, only once, at 3, 5, and 7 seconds
            // TimersManager.SetTimer(this, 1f, TestPrintTime);
            // TimersManager.SetTimer(this, 3f, TestPrintTime);
            // TimersManager.SetTimer(this, 5f, TestPrintTime);


            // Set a timer that calls Timer2() once every second
            Timer t2 = new Timer(this, 1f, Timer.INFINITE_LOOPS, false, TestPrintTime);
            TimersManager.SetTimer(t2);

            // This will override the previous one,
            // calling Timer2() after 3 seconds ONLY ONCE!
            TimersManager.SetTimer(this, 3f, TestPrintTime);
        }

        void Start()
        { }

        public void TestPrint1()
        {
            Debug.Log("尝试每3秒输出一次！");
        }

        public void TestPrint2()
        {
            Debug.Log("尝试每5秒输出一次！");
        }

        public void TestPrint3()
        {
            Debug.Log("尝试每10秒输出一次！");
        }

        public void TestPrintTime()
        {
            Debug.Log(DateTime.Now);
        }
    }
}