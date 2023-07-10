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
    [SerializeField] private GameObject _canvas;
    private Transform _checkpoint;

    public bool GameStarted { get; set; } = false;

    public Grid Grid { get; private set; }
    public FreezeTracker FreezeTracker { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        _canvas.SetActive(false);
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
        Grid = (Grid)FindObjectOfType(typeof(Grid));
        FreezeTracker = (FreezeTracker)FindObjectOfType(typeof(FreezeTracker));

        SpawnPlayer();
        _canvas.SetActive(false);
        StartCoroutine(FinishedLoadLevel());
        Debug.Log(GameStarted);
    }

    private IEnumerator LoadLevel(int LevelIndex)
    {
        _canvas.SetActive(true);
        _transition.SetTrigger("StartFade");

        yield return new WaitForSeconds(1);
        _canvas.SetActive(false);
        SceneManager.LoadScene(LevelIndex);
    }

    private IEnumerator FinishedLoadLevel()
    {
        _canvas.SetActive(true);
        _transition.SetTrigger("EndFade");

        yield return new WaitForSeconds(1);

        _canvas.SetActive(false);
    }
}
