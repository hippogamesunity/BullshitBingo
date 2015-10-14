using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class LogCallback : Script
    {
        public event Action<string, string, LogType> Callback = (condition, stacktrace, type) => { };
        private static LogCallback _instance;

        public static LogCallback Instance
        {
            get { return _instance ?? (_instance = new GameObject(typeof(LogCallback).ToString()).AddComponent<LogCallback>()); }
        }

        public void Start()
        {
            Application.RegisterLogCallback((condition, stacktrace, type) => Callback(condition, stacktrace, type));
        }
    }
}