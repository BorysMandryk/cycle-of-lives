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

    // ��� ������ �� ����, �� ��� �� ����������� �� ��'��� ������
    private void Freeze()
    {
        _onFreeze?.Invoke();

        Vector2 freezePos = transform.position;  // ����� ������� ���� �������

        Instantiate(_freezedPrefab, freezePos, Quaternion.identity);

        Destroy(gameObject);
    }
}
