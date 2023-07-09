using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CycleFreezer : MonoBehaviour
{
    //[SerializeField] private GameObject _freezedPrefab;
    [SerializeField] private FreezeType _freezedType;
    //[SerializeField] private UnityEvent _onFreeze;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_playerInput.FreezePressed)
        {
            Freeze();
        }
    }

    // “ут маЇтьс€ на уваз≥, що все це знаходитьс€ на об'Їкт≥ гравц€
    private void Freeze()
    {
        Vector2 freezePos = Utils.SnapToGrid(GameManager.Instance.Grid, transform.position);
        GameObject instance = Instantiate(_freezedType.gameObject, freezePos, Quaternion.identity);
        instance.GetComponent<FreezeType>().Freeze();

        GameManager.Instance.FreezeTracker.AddFreeze(instance);

        GameManager.Instance.SpawnPlayer();
        Destroy(gameObject);
    }
}
