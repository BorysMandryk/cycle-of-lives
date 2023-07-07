using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CycleFreezer : MonoBehaviour
{
    [SerializeField] private GameObject _freezedPrefab;

    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();

        _controls.Player.Freeze.performed += Freeze;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    // ��� ������ �� ����, �� ��� �� ����������� �� ��'��� ������
    private void Freeze(InputAction.CallbackContext context)
    {
        Vector2 freezePos = transform.position;  // ����� ������� ���� �������

        Instantiate(_freezedPrefab, freezePos, Quaternion.identity);

        Destroy(gameObject);
    }
}
