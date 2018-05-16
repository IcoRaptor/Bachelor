using Framework;
using UnityEngine;

public class PositionCollection : SingletonAsObject<PositionCollection>
{
    #region Variables

    [SerializeField]
    private Transform _work = null;
    [SerializeField]
    private Transform _shop = null;
    [SerializeField]
    private Transform _playground = null;
    [SerializeField]
    private Transform _school = null;
    [SerializeField]
    private Transform _bar = null;

    #endregion

    #region Properties

    public static PositionCollection Instance
    {
        get { return (PositionCollection)_Instance; }
    }

    public Transform Work { get { return _work; } }
    public Transform Shop { get { return _shop; } }
    public Transform Playground { get { return _playground; } }
    public Transform School { get { return _school; } }
    public Transform Bar { get { return _bar; } }

    #endregion
}