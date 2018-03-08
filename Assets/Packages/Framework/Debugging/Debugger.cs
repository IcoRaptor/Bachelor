using System.Diagnostics;

namespace Framework.Debugging
{
    /// <summary>
    /// Provides functions for debugging
    /// </summary>
    public static class Debugger
    {
        #region Variables

        private const string _CONDITIONAL_STRING = "UNITY_EDITOR";

        #endregion

        /// <summary>
        /// Logs a message to the Unity Console
        ///  (Conditional: UNITY_EDITOR)
        /// </summary>
        /// <param name="logType">The type of log message</param>
        /// <param name="msg">Message</param>
        [Conditional(_CONDITIONAL_STRING)]
        public static void Log(LOG_TYPE logType, object msg)
        {
            switch (logType)
            {
                case LOG_TYPE.LOG:
                    UnityEngine.Debug.Log(msg);
                    break;

                case LOG_TYPE.WARNING:
                    UnityEngine.Debug.LogWarning(msg);
                    break;

                case LOG_TYPE.ERROR:
                    UnityEngine.Debug.LogError(msg);
                    break;

                case LOG_TYPE.ASSERT:
                    UnityEngine.Debug.LogAssertion(msg);
                    break;
            }
        }

        /// <summary>
        /// Logs a message to the Unity Console
        ///  (Conditional: UNITY_EDITOR)
        /// </summary>
        /// <param name="logType">The type of log message</param>
        /// <param name="msg">Message</param>
        /// <param name="args">Arguments</param>
        [Conditional(_CONDITIONAL_STRING)]
        public static void LogFormat(LOG_TYPE logType, string msg, params object[] args)
        {
            switch (logType)
            {
                case LOG_TYPE.LOG:
                    UnityEngine.Debug.LogFormat(msg, args);
                    break;

                case LOG_TYPE.WARNING:
                    UnityEngine.Debug.LogWarningFormat(msg, args);
                    break;

                case LOG_TYPE.ERROR:
                    UnityEngine.Debug.LogErrorFormat(msg, args);
                    break;

                case LOG_TYPE.ASSERT:
                    UnityEngine.Debug.LogAssertionFormat(msg, args);
                    break;
            }
        }
    }
}