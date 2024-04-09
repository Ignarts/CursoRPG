using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPoint;

        private void Awake()
        {
            PlayerLife.OnPlayerDefeated += RespawnPlayer;
        }

        private void RespawnPlayer()
        {
            StartCoroutine(RespawnPlayerAtPosition());
        }

        private IEnumerator RespawnPlayerAtPosition()
        {
            yield return new WaitForSeconds(1.5f);
            var playerLife = PlayerLife.Instance;

            playerLife.Heal(playerLife.MaxLife);
            playerLife.transform.position = _playerSpawnPoint.position;

            PlayerMana.Instance.RegenerateAllMana();
        }
    }
}