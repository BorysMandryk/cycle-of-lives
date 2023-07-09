using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FreezeType : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFreeze;

    public virtual void Freeze()
    {
        _onFreeze?.Invoke();
    }
}