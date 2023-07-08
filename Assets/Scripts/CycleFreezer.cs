using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CycleFreezer : MonoBehaviour
{
    [SerializeField] private GameObject _freezedPrefab;
    [SerializeField] private UnityEvent _onFreeze;

    //[SerializeField] private Grid _grid;

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
        _onFreeze?.Invoke();

        
        Vector2 freezePos = SnapToGrid(transform.position);

        Instantiate(_freezedPrefab, freezePos, Quaternion.identity);

        Destroy(gameObject);
    }

    private Vector2 SnapToGrid(Vector2 position)
    {
        Vector3Int cellPos = GameManager.Instance.Grid.WorldToCell(position);
        return GameManager.Instance.Grid.GetCellCenterWorld(cellPos);
    }
}
