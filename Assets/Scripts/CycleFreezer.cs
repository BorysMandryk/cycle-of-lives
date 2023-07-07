using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CycleFreezer : MonoBehaviour
{
    [SerializeField] private GameObject _freezedPrefab;
    [SerializeField] private UnityEvent _onFreeze;

    private Controls _controls;

    private void OnEnable()
    {
        _controls = new Controls();

        _controls.Player.Freeze.performed += Freeze;
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Freeze.performed -= Freeze;
        _controls.Player.Disable();
    }

    // Тут мається на увазі, що все це знаходиться на об'єкті гравця
    private void Freeze(InputAction.CallbackContext context)
    {
        _onFreeze?.Invoke();

        Vector2 freezePos = transform.position;  // Можна вибрати іншу позицію

        Instantiate(_freezedPrefab, freezePos, Quaternion.identity);

        Destroy(gameObject);
    }
}
