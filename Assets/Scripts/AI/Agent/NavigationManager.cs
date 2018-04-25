using Framework.Debugging;
using Pathfinding;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Seeker), typeof(AIDestinationSetter))]
    public class NavigationManager : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        private Blackboard _blackboard;
        private AIDestinationSetter _dest;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponentInParent<Blackboard>();
            _dest = GetComponent<AIDestinationSetter>();

            if (!_blackboard)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                   "{0}: {1} missing!\n",
                    gameObject.name, typeof(Blackboard).Name);
            }
        }
    }
}