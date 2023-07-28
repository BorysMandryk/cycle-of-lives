using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CycleFreezer : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    private FreezeTypeSelector _freezeTypeSelector;
    //[SerializeField] private FreezeType _freezedType;

    private void Awake()
    {
        _freezeTypeSelector = FindObjectOfType<FreezeTypeSelector>();
    }

    private void OnEnable()
    {
        _inputReader.FreezeEvent += Freeze;
    }

    private void OnDisable()
    {
        _inputReader.FreezeEvent -= Freeze;
    }

    private void Freeze()
    {
        CreateFreezeObject();
        DestroyOnFreeze();
    }

    private void DestroyOnFreeze()
    {
        Destroy(gameObject);
        GameManager.Instance.SpawnPlayer();
    }

    private void CreateFreezeObject()
    {
        Vector2 freezePos = Utils.SnapToGrid(GameManager.Instance.Grid, transform.position);

        GameObject _freezeGO = _freezeTypeSelector.CurrentFreezeType.gameObject;
        GameObject instance = Instantiate(_freezeGO, freezePos, Quaternion.identity);
        instance.GetComponent<FreezeType>().Freeze();

        GameManager.Instance.FreezeTracker.AddFreeze(instance);
    }
}
