using Framework.Debugging;
using Pathfinding;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(AIDestinationSetter), typeof(AILerp))]
    public class NavigationManager : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        private Blackboard _blackboard;
        private AIDestinationSetter _dest;
        private AILerp _move;
#pragma warning restore

        #endregion

        private void Awake()
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
            if (_blackboard.InteractionInterrupt)
            {
                if (_move.gameObject.activeSelf)
                    _move.gameObject.SetActive(false);
            }
            else
            {
                if (!_move.gameObject.activeSelf)
                    _move.gameObject.SetActive(true);
            }
        }
    }
}