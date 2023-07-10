using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int _nextLevelIndex;
    [SerializeField] private Transform _nextLevelSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.SetDefaultSpawnPosition(_nextLevelSpawnPoint.position);
        GameManager.Instance.LoadNextLevel(_nextLevelIndex);
    }
}
