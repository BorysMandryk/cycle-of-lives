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

        // ������ 1
        // �������� ����� ������ � ����, �� �� ������ ���������� �� ������,
        // �� � ��� �� �� �������
        //PlayerPrefs.SetString("LastExitName", _exitName);

        // ������ 2
        GameManager.Instance.LastExitName = _exitName;

        //GameManager.Instance.LoadNextLevel(_nextLevelIndex);
        GameManager.Instance.LoadNextLevel(_nextSceneName);
    }
}
