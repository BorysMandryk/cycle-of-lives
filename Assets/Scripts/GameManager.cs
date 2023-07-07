using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private Vector2 _spawnPos;

    public void SpawnPlayer()
    {
        Instantiate(_playerPrefab, _spawnPos, Quaternion.identity);
    }
}
