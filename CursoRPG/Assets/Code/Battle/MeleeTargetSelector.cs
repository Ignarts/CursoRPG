using System;
using Entities.AI;
using UnityEngine;

namespace Battle
{
    public class MeleeTargetSelector : MonoBehaviour
    {
        #region Private Attributes
        #endregion

        #region Properties

        public EnemyInteraction TargetEnemy { get; private set; }
        
        #endregion

        #region Events

        public static event Action<EnemyInteraction> OnEnemyDetected;
        public static event Action OnEnemyLost;
        
        #endregion
        
        #region MonoBehaviour Methods

        private void OnTriggerEnter2D(Collider2D other)
        {            
            if(other.CompareTag("Enemy"))
            {
                TargetEnemy = other.GetComponent<EnemyInteraction>();

                if(TargetEnemy.GetComponent<EnemyLife>().CurrentLife <= 0)
                    return;

                OnEnemyDetected?.Invoke(TargetEnemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Enemy"))
            {
                if(TargetEnemy.GetComponent<EnemyLife>().CurrentLife <= 0)
                    return;
                
                OnEnemyLost?.Invoke();
            }
        }

        #endregion
    }
}
