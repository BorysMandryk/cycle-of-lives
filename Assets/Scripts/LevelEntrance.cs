using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    [SerializeField] private string _lastExitName;

    private void Start()
    {
        if (_lastExitName == PlayerPrefs.GetString("LastExitName"))
        {
            GameManager.Instance.SetDefaultSpawnPosition(transform.position);
            GameManager.Instance.SpawnPlayer();
        }
    }
}