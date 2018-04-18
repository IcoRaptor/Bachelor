using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Blackboard), typeof(WorkingMemory))]
    public class AIAgent : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        private Blackboard _blackboard;
        private WorkingMemory _memory;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
            _memory = GetComponent<WorkingMemory>();
        }
    }
}