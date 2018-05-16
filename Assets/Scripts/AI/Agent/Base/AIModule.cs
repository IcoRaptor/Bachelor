using Framework.Debugging;
using UnityEngine;

namespace AI
{
    public abstract class AIModule : MonoBehaviour
    {
        #region Properties

        public Blackboard Board { get; private set; }
        public WorkingMemory Memory { get; private set; }

        #endregion

        private void Awake()
        {
            Board = GetComponentInParent<Blackboard>();
            Memory = GetComponentInParent<WorkingMemory>();

            if (!Board)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                   "{0}: {1} missing!\n",
                    gameObject.name, typeof(Blackboard).Name);
            }

            if (!Memory)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                   "{0}: {1} missing!\n",
                    gameObject.name, typeof(WorkingMemory).Name);
            }
        }

        public abstract void Abort();
    }
}