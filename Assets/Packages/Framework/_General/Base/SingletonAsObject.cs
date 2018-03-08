using UnityEngine;

namespace Framework
{
    /// <summary>
    /// Base class for singletons as objects
    /// </summary>
    /// <typeparam name="T">Derived class</typeparam>
    public abstract class SingletonAsObject<T> : MonoBehaviour
        where T : SingletonAsObject<T>
    {
        #region Variables

        private static T _instance;

        #endregion

        #region Properties

        protected static SingletonAsObject<T> _Instance
        {
            get { return _instance; }
        }

        #endregion

        private void Awake()
        {
            WakeUp();
        }

        /// <summary>
        /// Can be used for custom Awake
        /// </summary>
        protected void WakeUp()
        {
            if (!_instance)
                _instance = (T)this;
            else if (_instance != (T)this)
                Destroy(gameObject);
        }
    }
}