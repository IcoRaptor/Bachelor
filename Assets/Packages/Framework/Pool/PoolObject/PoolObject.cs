using UnityEngine;

namespace Framework.Pool
{
    /// <summary>
    /// Base class for PoolObjects
    /// </summary>
    public class PoolObject : MonoBehaviour, IPoolObject
    {
        #region Variables

        private int _instanceID;

        #endregion

        #region Properties

        public int InstanceID
        {
            get { return _instanceID; }
            set { _instanceID = value; }
        }

        #endregion

        public virtual void OnObjectReuse()
        {
        }

        /// <summary>
        /// Increases the object count in the PoolManager
        /// </summary>
        /// <param name="poolObj">PoolObject</param>
        public void IncreaseObjectCount(GameObject poolObj)
        {
            if (PoolManager.Instance.ActiveObjects.ContainsKey(_instanceID))
                ++PoolManager.Instance.ActiveObjects[_instanceID][1];
        }

        /// <summary>
        /// Decreases the object count in the PoolManager
        /// </summary>
        /// <param name="poolObj">PoolObject</param>
        public void DecreaseObjectCount(GameObject poolObj)
        {
            if (PoolManager.Instance.ActiveObjects.ContainsKey(_instanceID))
                --PoolManager.Instance.ActiveObjects[_instanceID][1];
        }

        private void OnDisable()
        {
            if (PoolManager.IsAlive)
                DecreaseObjectCount(gameObject);
        }
    }
}