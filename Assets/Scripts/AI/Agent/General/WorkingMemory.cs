using UnityEngine;

public class WorkingMemory : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform _home = null;
    [SerializeField]
    private Transform[] _patrolPoints = null;

    #endregion

    #region Properties

    public Transform Home { get { return _home; } }
    public Transform[] PatrolPoints { get { return _patrolPoints; } }

    public Transform Work { get; private set; }
    public Transform School { get; private set; }
    public Transform Playground { get; private set; }
    public Transform Shop { get; private set; }
    public Transform Bar { get; private set; }

    #endregion

    private void Start()
    {
        Work = PositionCollection.Instance.Work;
        School = PositionCollection.Instance.School;
        Playground = PositionCollection.Instance.Playground;
        Shop = PositionCollection.Instance.Shop;
        Bar = PositionCollection.Instance.Bar;
    }
}