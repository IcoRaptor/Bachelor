using Framework.Debugging;
using Pathfinding;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(AIDestinationSetter), typeof(AILerp))]
    public class NavigationManager : MonoBehaviour
    {
        #region Variables

        private Blackboard _blackboard;
        private AIDestinationSetter _dest;
        private AILerp _move;

        #endregion

        private void Start()
        {
            _blackboard = GetComponentInParent<Blackboard>();
            _dest = GetComponent<AIDestinationSetter>();
            _move = GetComponent<AILerp>();

            if (!_blackboard)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                   "{0}: {1} missing!\n",
                    gameObject.name, typeof(Blackboard).Name);
            }
        }

        private void Update()
        {
            CheckInteractionInterrupt();
            CheckDestinationChange();
        }

        private void CheckInteractionInterrupt()
        {
            if (_blackboard.InteractionInterrupt)
            {
                if (_move.isActiveAndEnabled)
                    _move.enabled = false;

                return;
            }

            if (!_move.isActiveAndEnabled)
                _move.enabled = true;
        }

        private void CheckDestinationChange()
        {
            if (_blackboard.ChangeDestination)
            {
                _dest.target = _blackboard.NextNavigationPoint;
                _blackboard.ChangeDestination = false;
            }
        }
    }
}