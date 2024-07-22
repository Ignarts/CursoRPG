using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Loot
{
    public class LootManager : MonoBehaviour
    {
        public static LootManager Instance;

        #region MonoBehavior Methods

        #region Private Attributes

        [Header("Loot Manager Configuration")]
        [SerializeField] private GameObject _lootPanel;
        [SerializeField] private LootButton _lootButtonPrefab;
        [SerializeField] private Transform _lootContainer;

        #endregion

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            HideLootPanel();
        }

        private void Update()
        {
            if(!_lootPanel.activeSelf)
                return;

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                HideLootPanel();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show the loot panel
        /// </summary>
        public void ShowLootPanel(EnemyLoot enemyLoot)
        {
            _lootPanel.SetActive(true);

            if (!IsContainerEmpty())
            {
                foreach (Transform child in _lootContainer.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (DropItem dropItem in enemyLoot.DropItemsSelected)
            {
                LoadLootItem(dropItem);
            }
        }

        /// <summary>
        /// Hide the loot panel
        /// </summary>
        private void HideLootPanel()
        {
            _lootPanel.SetActive(false);
        }

        private void LoadLootItem(DropItem dropItem)
        {
            if (dropItem.ItemPicked)
                return;

            LootButton lootButton = Instantiate(_lootButtonPrefab, _lootContainer);
            lootButton.ConfigureLootItem(dropItem);
        }

        private bool IsContainerEmpty()
        {
            LootButton[] children = _lootContainer.GetComponentsInChildren<LootButton>();

            return children.Length == 0;
        }

        #endregion
    }
}
