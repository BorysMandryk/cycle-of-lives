using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector2 _defaultSpawnPos;
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
        DontDestroyOnLoad(gameObject);

        Grid = (Grid)FindObjectOfType(typeof(Grid));
        FreezeTracker = (FreezeTracker)FindObjectOfType(typeof(FreezeTracker));
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void SpawnPlayer()
    {
        Vector2 spawnPos;
        if (_checkpoint != null)
        {
            spawnPos = _checkpoint.position;
        }
        else
        {
            spawnPos = _defaultSpawnPos;
        }

        Instantiate(_playerPrefab, spawnPos, Quaternion.identity);
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        _checkpoint = newCheckpoint;
    }

    public void SetDefaultSpawnPosition(Vector2 newPosition)
    {
        _defaultSpawnPos = newPosition;
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        _transition.SetTrigger("EndFade");
    }

    private IEnumerator LoadLevel(int LevelIndex)
    {
        _transition.SetTrigger("StartFade");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(LevelIndex);
    }
}
