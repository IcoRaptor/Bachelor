using UnityEngine;

namespace AI
{
    /// <summary>
    /// Communication hub of the agent.
    /// </summary>
    public class Blackboard : MonoBehaviour
    {
        /// <summary>
        /// Dialog option of the active action
        /// </summary>
        public string Dialog { get; set; }

        /// <summary>
        /// Stop moving on interaction with player
        /// </summary>
        public bool InteractionInterrupt { get; set; }

        /// <summary>
        /// The next navigation point
        /// </summary>
        public Transform NextNavigationPoint { get; set; }

        /// <summary>
        /// Is a new navigation point available
        /// </summary>
        public bool ChangeDestination { get; set; }
    }
}