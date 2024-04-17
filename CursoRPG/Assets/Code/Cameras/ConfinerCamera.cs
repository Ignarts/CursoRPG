using UnityEngine;

public class ConfinerCamera : MonoBehaviour
{
    #region Private Attributes

    [SerializeField] private GameObject _virtualCamera;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        _virtualCamera.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _virtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _virtualCamera.SetActive(false);
        }
    }

    #endregion
}
