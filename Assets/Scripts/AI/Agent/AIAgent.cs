using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Blackboard))]
    public class AIAgent : MonoBehaviour
    {
        #region Variables

#pragma warning disable 0414
        private Blackboard _blackboard;
#pragma warning restore

        #endregion

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
        }
    }
}