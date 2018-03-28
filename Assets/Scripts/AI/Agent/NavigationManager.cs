using Framework.Debugging;
using Pathfinding;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Seeker))]
    public class NavigationManager : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        private Blackboard _blackboard;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponentInParent<Blackboard>();

            if (!_blackboard)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                   "{0}: {1} missing!\n",
                    gameObject.name, _blackboard.GetType().Name);
            }
        }
    }
}