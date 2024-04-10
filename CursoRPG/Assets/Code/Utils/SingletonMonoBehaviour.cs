using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Base class for singletons in the game that derive from MonoBehaviour. It takes provides some basic methods to access the 
    /// singleton instance as well as marking it as don't destroy on load in the Awake method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();
                }

                return instance;
            }
        }


        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                // trying to create a second music manager, early quit and destroy this object
                //Debug.LogWarningFormat("There's already an instance of {0} available in the game. Destroying the new one", GetType().ToString());

                Destroy(gameObject);
                return;
            }

            instance = this as T;

            // make sure the game object won't get destroyed when loading other scenes
            // NOTE: [Barkley] Override the Awake method if you want to ensure the singleton lives in the root
            if (transform.parent == null)
                DontDestroyOnLoad(gameObject);
        }
    }
}
