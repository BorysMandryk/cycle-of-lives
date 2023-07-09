using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _defaultSpawnPoint;
    [SerializeField] private Animator _transition;
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
        Transform spawnPoint;
        if (_checkpoint != null)
        {
            spawnPoint = _checkpoint;
        }
        else
        {
            spawnPoint = _defaultSpawnPoint;
        }

        Instantiate(_playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        _checkpoint = newCheckpoint;
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    private IEnumerator LoadLevel(int LevelIndex)
    {
        _transition.SetTrigger("StartFade");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(LevelIndex);
    }
}
