using Framework.Debugging;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Blackboard))]
    public abstract class BaseAgent : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        protected Blackboard _blackboard;
        protected AIModule _ai;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
            _ai = GetComponentInChildren<AIModule>();

            if (!_ai)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                    "{0}: {1} missing!\n",
                    gameObject.name, _ai.GetType().Name);
            }
        }
    }
}