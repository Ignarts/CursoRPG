using Entities.AI;
using Player;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private Vector2 direction;
        private EnemyInteraction targetEnemy;
        
        #endregion

        #region Properties

        public PlayerAttack PlayerAttack { get; set; }
        
        #endregion

        #region MonoBehaviour Methods

        private void FixedUpdate()
        {
            if (targetEnemy == null)
                return;

            MoveProjectile();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                float damage = PlayerAttack.DamageDealt();
                EnemyLife enemyLife = targetEnemy.GetComponent<EnemyLife>();
                enemyLife.TakeDamage(damage);
                PlayerAttack.DealtDamageEvent(enemyLife);
                gameObject.SetActive(false);
            }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Initialize the projectile with the target enemy
        /// </summary>
        /// <param name="enemyInteraction"></param>
        public void InitProjectile(PlayerAttack attack)
        {
            PlayerAttack = attack;
            targetEnemy = PlayerAttack.TargetEnemy;
        }

        /// <summary>
        /// Move the projectile to the target enemy
        /// </summary>
        private void MoveProjectile()
        {
            direction = targetEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 newPosition = _rigidbody2D.position + (direction.normalized * _speed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(newPosition);
        }
        
        #endregion
    }
}
