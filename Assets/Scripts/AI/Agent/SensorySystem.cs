using Framework.Debugging;
using UnityEngine;

namespace AI
{
    public class SensorySystem : MonoBehaviour
    {
        #region Variables

        private WorkingMemory _memory;

        #endregion

        private void Awake()
        {
            _memory = GetComponentInParent<WorkingMemory>();

            if (!_memory)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                   "{0}: {1} missing!\n",
                    gameObject.name, typeof(WorkingMemory).Name);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var memoryFact = other.GetComponent<IMemoryFact>();

            if (memoryFact == null)
                return;

            memoryFact.WriteToMemory(_memory);
        }
    }
}