using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTypeSelector : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private List<FreezeType> _freezeTypes = new List<FreezeType>();
    [SerializeField] private List<GameObject> _freezeTypesUI = new List<GameObject>();
    
    private int _currentIndex;

    public FreezeType CurrentFreezeType => _freezeTypes[_currentIndex];

    private void Awake()
    {
        _inputReader.ChangeFreezeTypeEvent += ChangeType;
        ChangeTypeUI();
    }

    private void OnDisable()
    {
        _inputReader.ChangeFreezeTypeEvent -= ChangeType;
    }

    public void ChangeType(float value)
    {
        _currentIndex += (int)value;
        if (_currentIndex >= _freezeTypes.Count)
        {
            _currentIndex = 0;
        }
        else if (_currentIndex < 0)
        {
            _currentIndex = _freezeTypes.Count - 1;
        }

        ChangeTypeUI();
        Debug.Log(CurrentFreezeType);
    }

    private void ChangeTypeUI()
    {
        for (int i = 0; i < _freezeTypesUI.Count; i++)
        {
            if (_currentIndex == i)
            {
                _freezeTypesUI[i].SetActive(true);
            }
            else
            {
                _freezeTypesUI[i].SetActive(false);
            }
        }
    }
}
