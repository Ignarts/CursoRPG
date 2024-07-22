using System;
using Entities.AI;
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
        }
        
        #endregion

        #region Methods


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
                    LootManager.Instance.ShowLootPanel();
                }
                
                OnEnemySelected?.Invoke(SelectedEnemy);
            }
        }
        
        #endregion
    }
}
