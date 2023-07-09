using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _defaultSpawnPoint;
    private Transform _checkpoint;

    public Grid Grid { get; private set; }
    public FreezeTracker FreezeTracker { get; private set; }

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
        FreezeTracker = (FreezeTracker)FindObjectOfType(typeof(FreezeTracker));
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = _checkpoint ?? _defaultSpawnPoint;
        Instantiate(_playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        _checkpoint = newCheckpoint;
    }
}
