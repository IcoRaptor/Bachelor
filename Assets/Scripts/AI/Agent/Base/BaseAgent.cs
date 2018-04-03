using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Blackboard))]
    public abstract class BaseAgent : MonoBehaviour
    {
        #region Variables

        protected Blackboard _blackboard;

        #endregion

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
        }
    }
}