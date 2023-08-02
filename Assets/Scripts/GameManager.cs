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

    private GameObject _currentPlayer;
    private Animator _playerAnimator;

    public bool GameStarted { get; set; } = false;

    public Grid Grid { get; private set; }
    public FreezeTracker FreezeTracker { get; private set; }

    public static GameManager Instance { get; private set; }
    public string LastExitName { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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

        _currentPlayer = Instantiate(_playerPrefab, spawnPos, Quaternion.identity);
        _playerAnimator = _currentPlayer.GetComponent<Animator>();
        _playerAnimator.SetTrigger("PlayerCreation");
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        _checkpoint = newCheckpoint;
    }

    public void SetDefaultSpawnPosition(Vector2 newPosition)
    {
        _defaultSpawnPos = newPosition;
    }

    public void LoadNextLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Grid = (Grid)FindObjectOfType(typeof(Grid));
        FreezeTracker = (FreezeTracker)FindObjectOfType(typeof(FreezeTracker));

        StartCoroutine(FinishedLoadLevel());
        Debug.Log(GameStarted);
    }

    private IEnumerator LoadLevel(string levelName)
    {
        _transition.SetTrigger("StartFade");
        //yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(_transition.GetCurrentAnimatorStateInfo(0).length);

        _transition.ResetTrigger("StartFade");
        SceneManager.LoadScene(levelName);
    }

    private IEnumerator FinishedLoadLevel()
    {
        _transition.SetTrigger("EndFade");

        yield return new WaitForSeconds(_transition.GetCurrentAnimatorStateInfo(0).length);

        _transition.ResetTrigger("EndFade");
    }
}
