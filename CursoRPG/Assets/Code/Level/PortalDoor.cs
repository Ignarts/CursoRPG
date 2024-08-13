using UnityEngine;

namespace Level
{
    public class PortalDoor : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Transform _destination;
        
        #endregion

        #region MonoBehaviour Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.localPosition = _destination.position;
            }
        }

        #endregion
    }
}