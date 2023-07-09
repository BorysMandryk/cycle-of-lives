using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTypeSelector : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private List<FreezeType> _freezeTypes = new List<FreezeType>();
    
    private int _currentIndex;

    public FreezeType CurrentFreezeType => _freezeTypes[_currentIndex];

    private void Awake()
    {
        _inputReader.ChangeFreezeTypeEvent += ChangeType;
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
        Debug.Log(CurrentFreezeType);
    }
}
