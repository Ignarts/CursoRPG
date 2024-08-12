using System;
using Entities.AI;
using Loot;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Battle
{
    public class TargetSelectionManager : MonoBehaviour
    {
        #region Private Attributes

        private Camera _mainCamera;
        
        #endregion

        #region Properties

        public static EnemyInteraction SelectedEnemy { get; private set; }
        
        #endregion

        #region Events

        public static event Action<EnemyInteraction> OnEnemySelected;
        public static event Action OnTargetNotSelected;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            SelectEnemy();

            if(SelectedEnemy != null && SelectedEnemy.GetComponent<EnemyLife>().IsDefeated())
            {
                SelectedEnemy = null;
                OnTargetNotSelected?.Invoke();
            }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Select the enemy that the player clicked on
        /// </summary>
        private void SelectEnemy()
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));

                if(hit.collider == null)
                {
                    OnTargetNotSelected?.Invoke();
                    return;
                }

                SelectedEnemy = hit.collider.GetComponent<EnemyInteraction>();
                EnemyLife enemyLife = SelectedEnemy.GetComponent<EnemyLife>();

                if(enemyLife.IsDefeated())
                {
                    EnemyLoot enemyLoot = SelectedEnemy.GetComponent<EnemyLoot>();
                    LootManager.Instance.ShowLootPanel(enemyLoot);
                }
                
                OnEnemySelected?.Invoke(SelectedEnemy);
            }
        }
        
        #endregion
    }
}
