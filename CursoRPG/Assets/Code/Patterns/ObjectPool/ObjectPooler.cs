using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Private Attributes

    [SerializeField] private int _poolSize;

    private List<GameObject> _pool;
    private GameObject _poolContainer;
    
    #endregion

    #region Properties

    public GameObject PoolContainer => _poolContainer;
    
    #endregion

    #region Methods

    /// <summary>
    /// Create the pooler with the object to pool
    /// </summary>
    /// <param name="objectToPool"></param>
    public void CreatePooler(GameObject objectToPool)
    {
        _pool = new List<GameObject>();
        _poolContainer = new GameObject($"PoolContainer-{objectToPool.name}");

        for (int i = 0; i < _poolSize; i++)
        {
            AddInstanceToPool(objectToPool);
        }
    }

    /// <summary>
    /// Add an instance of the object to the pool
    /// </summary>
    /// <param name="objectToPool"></param>
    /// <returns></returns>
    private GameObject AddInstanceToPool(GameObject objectToPool)
    {
        GameObject newObject = Instantiate(objectToPool, _poolContainer.transform);
        newObject.SetActive(false);
        _pool.Add(newObject);
        return newObject;
    }

    /// <summary>
    /// Get an instance of the object from the pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstance()
    {
        for(int i = 0; i < _pool.Count; i++)
        {
            if(!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Clear the pool
    /// </summary>
    public void ClearPool()
    {
        Destroy(_poolContainer);
        _pool.Clear();
    }
    
    #endregion
}
