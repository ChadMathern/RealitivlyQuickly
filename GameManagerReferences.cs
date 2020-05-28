using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class GameManagerReferences : MonoBehaviour
    {
        public string playerTag;
        public static string _playerTag;

        public string enemyTag;
        public static string _EnemyTag;

        public static GameObject _player;

        public void Awake()
        {
            if (playerTag == "")
            {
                Debug.LogWarning("Type name of player tag in GameManagerRefrences inspector");
            }

            if (enemyTag == "")
            {
                Debug.LogWarning("Type name of Enemy tag in GameManagerRefrences inspector");
            }

            _playerTag = playerTag;
            _EnemyTag = enemyTag;

            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
