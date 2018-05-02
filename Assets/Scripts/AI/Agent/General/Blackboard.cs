using UnityEngine;

namespace AI
{
    /// <summary>
    /// Central hub of the agent.
    ///  Provides a number of properties
    /// </summary>
    public class Blackboard : MonoBehaviour
    {
        #region Properties

        public string Dialog { get; set; }

        #endregion
    }
}