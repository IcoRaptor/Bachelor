using UnityEngine;

namespace AI
{
    /// <summary>
    /// Central hub of the agent.
    ///  Provides a number of properties
    /// </summary>
    public class Blackboard : MonoBehaviour
    {
        #region Interactable

        /// <summary>
        /// Dialog option of the active action
        /// </summary>
        public string Dialog { get; set; }

        /// <summary>
        /// Stop moving on interaction with player
        /// </summary>
        public bool InteractionInterrupt { get; set; }

        #endregion
    }
}