using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Blackboard), typeof(AIModule))]
    public abstract class BaseAgent : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        private Blackboard _blackboard;
        private AIModule _ai;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
            _ai = GetComponent<AIModule>();
        }
    }
}