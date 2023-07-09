using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private Vector2 _spawnPos;

    public InputManager InputManager { get; private set; }
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

        InputManager = (InputManager)FindObjectOfType(typeof(InputManager));
        Grid = (Grid)FindObjectOfType(typeof(Grid));
        FreezeTracker = (FreezeTracker)FindObjectOfType(typeof(FreezeTracker));
    }

    public void SpawnPlayer()
    {
        Instantiate(_playerPrefab, _spawnPos, Quaternion.identity);
    }
}
