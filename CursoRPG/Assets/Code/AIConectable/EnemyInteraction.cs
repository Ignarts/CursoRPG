using UnityEngine;

namespace Entities.AI
{
    public class EnemyInteraction : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _selectedRangeIndicator;
        [SerializeField] private GameObject _selectedMeleeIndicator;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            ShowRangeSelectedIndicator(false);
            ShowMeleeSelectedIndicator(false);
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Show or hide the selected indicator for range weapons
        /// </summary>
        /// <param name="show"></param>
        public void ShowRangeSelectedIndicator(bool show)
        {
            _selectedRangeIndicator.SetActive(show);
        }

        /// <summary>
        /// Show or hide the selected indicator for melee weapons
        /// </summary>
        /// <param name="show"></param>
        public void ShowMeleeSelectedIndicator(bool show)
        {
            _selectedMeleeIndicator.SetActive(show);
        }

        public void DeactivateSelectedIndicators()
        {
            ShowRangeSelectedIndicator(false);
            ShowMeleeSelectedIndicator(false);
        }
        
        #endregion
    }
}
