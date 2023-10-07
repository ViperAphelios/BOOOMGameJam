using Micosmo.SensorToolkit;
using UnityEngine;

namespace ZFramework.TestExample.Scripts
{
    public class TestGravityTime : MonoBehaviour
    {
        public float startTime;

        public RaySensor2D raySensor2D;

        public bool stop;

        // Update is called once per frame
        private void Update()
        {
            startTime += Time.deltaTime;
        }

        public void PrintTime()
        {
            Debug.Log(startTime);
        }
    }
}