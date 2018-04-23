using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory : MonoBehaviour
{
    #region Variables

    private List<WorkingMemoryFact> _facts =
        new List<WorkingMemoryFact>();

    #endregion

    public void AddMemoryFact(WorkingMemoryFact fact)
    {
        _facts.Add(fact);
    }
}