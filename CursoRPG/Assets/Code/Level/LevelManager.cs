using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
#region Private Attributes

        [SerializeField]
        private Transform _playerSpawnPoint;

        [SerializeField] private PlayerMana _playerMana;
        [SerializeField] private PlayerLife _playerLife;

#endregion

#region MonoBehaviour Methods

        private void Awake()
        {
            PlayerLife.OnPlayerDefeated += RespawnPlayer;
        }

#endregion

#region Methods

        private void RespawnPlayer()
        {
            StartCoroutine(RespawnPlayerAtPosition());
        }

        private IEnumerator RespawnPlayerAtPosition()
        {
            yield return new WaitForSeconds(1.5f);

            _playerLife.Heal(_playerLife.MaxLife);
            _playerLife.transform.position = _playerSpawnPoint.position;

            _playerMana.RegenerateAllMana();
        }

#endregion
    }
}