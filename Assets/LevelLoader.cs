using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int _nextLevelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.LoadNextLevel(_nextLevelIndex);
    }
}
