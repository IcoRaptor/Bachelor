using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pool
{
    /// <summary>
    /// Creates pools in the Hierarchy
    /// </summary>
    public sealed class PoolObjectManager : SingletonAsObject<PoolObjectManager>
    {
        #region Variables

        [Header("Objects")]
        [SerializeField]
        private List<GameObject> _poolObjects = new List<GameObject>();

        [Header("Sizes")]
        [SerializeField]
        private List<uint> _poolSizes = new List<uint>();

        #endregion

        #region Properties

        public static PoolObjectManager Instance
        {
            get { return (PoolObjectManager)_Instance; }
        }

        #endregion

        private void Start()
        {
            for (int i = 0; i < _poolObjects.Count; ++i)
                PoolManager.Instance.CreatePool(_poolObjects[i], _poolSizes[i]);
        }

        private void OnValidate()
        {
            while (_poolSizes.Count < _poolObjects.Count)
                _poolSizes.Add(1);

            while (_poolSizes.Count > _poolObjects.Count)
                _poolSizes.RemoveLast();

            for (int i = 0; i < _poolSizes.Count; ++i)
                if (_poolSizes[i] == 0)
                    _poolSizes[i] = 1;
        }
    }
}