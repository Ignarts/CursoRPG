using UnityEngine;

namespace Entities.AI
{
    public class EnemyInteraction : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _selectedIndicator;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            ShowSelectedIndicator(false);
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Show or hide the selected indicator
        /// </summary>
        /// <param name="show"></param>
        public void ShowSelectedIndicator(bool show)
        {
            _selectedIndicator.SetActive(show);
        }
        
        #endregion
    }
}
