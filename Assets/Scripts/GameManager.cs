using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private Vector2 _spawnPos;

    public Grid Grid { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        Grid = (Grid)FindObjectOfType(typeof(Grid));
    }

    public void SpawnPlayer()
    {
        Instantiate(_playerPrefab, _spawnPos, Quaternion.identity);
    }
}
