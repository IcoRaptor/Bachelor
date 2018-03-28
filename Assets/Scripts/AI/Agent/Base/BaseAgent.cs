using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Blackboard))]
    public abstract class BaseAgent : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        protected Blackboard _blackboard;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
        }
    }
}