using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CycleFreezer : MonoBehaviour
{
    [SerializeField] private GameObject _freezedPrefab;
    [SerializeField] private UnityEvent _onFreeze;

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

    // Тут мається на увазі, що все це знаходиться на об'єкті гравця
    private void Freeze()
    {
        _onFreeze?.Invoke();

        Vector2 freezePos = transform.position;  // Можна вибрати іншу позицію

        Instantiate(_freezedPrefab, freezePos, Quaternion.identity);

        Destroy(gameObject);
    }
}
