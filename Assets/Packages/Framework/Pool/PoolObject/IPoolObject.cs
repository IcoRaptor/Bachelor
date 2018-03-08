using UnityEngine;

namespace Framework.Pool
{
    /// <summary>
    /// Interface for PoolObjects
    /// </summary>
    public interface IPoolObject
    {
        int InstanceID { get; set; }

        void OnObjectReuse();

        void IncreaseObjectCount(GameObject poolObj);

        void DecreaseObjectCount(GameObject poolObj);
    }
}