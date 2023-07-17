using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int _nextLevelIndex;
    [SerializeField] private string _nextSceneName;
    [SerializeField] private string _exitName;
    [SerializeField] private Transform _nextLevelSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Варіант 1
        PlayerPrefs.SetString("LastExitName", _exitName);

        // Варіант 2
        //GameManager.Instance.LastExitName = _exitName;

        //GameManager.Instance.LoadNextLevel(_nextLevelIndex);
        GameManager.Instance.LoadNextLevel(_nextSceneName);
    }
}
