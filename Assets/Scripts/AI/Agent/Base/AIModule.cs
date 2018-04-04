using Framework.Debugging;
using UnityEngine;

namespace AI
{
    public abstract class AIModule : MonoBehaviour
    {
        #region Variables

        protected Blackboard _blackboard;

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