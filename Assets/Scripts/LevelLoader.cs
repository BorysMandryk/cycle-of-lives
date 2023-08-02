using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;
    [SerializeField] private string _exitName;

    private bool _triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_triggered)
        {
            return;
        }
        _triggered = true;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        // Варіант 1
        // Проблема цього методу в тому, що він зберігає інформацію між сесіями,
        // що в цій грі не потрібно
        //PlayerPrefs.SetString("LastExitName", _exitName);

        // Варіант 2
        GameManager.Instance.LastExitName = _exitName;

        //GameManager.Instance.LoadNextLevel(_nextLevelIndex);
        GameManager.Instance.LoadNextLevel(_nextSceneName);
    }
}
