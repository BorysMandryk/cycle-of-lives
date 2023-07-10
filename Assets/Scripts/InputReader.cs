using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
{
    private Controls _controls;

    public event Action<float> MoveEvent;

    public event Action JumpEvent;
    public event Action JumpCancelledEvent;

    public event Action FreezeEvent;

    public event Action<float> ChangeFreezeTypeEvent;

    public event Action PauseMenuEvent;

    public event Action ReturnEvent;

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.UI.SetCallbacks(this);

        //SetPlayerMap();
        SetUIMap();
    }

    public void SetPlayerMap()
    {
        _controls.Player.Enable();
        _controls.UI.Disable();
    }

    public void SetUIMap()
    {
        _controls.Player.Disable();
        _controls.UI.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnChangeFreezeType(InputAction.CallbackContext context)
    {
        ChangeFreezeTypeEvent?.Invoke(context.ReadValue<float>());
    }

    public void OnFreeze(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FreezeEvent?.Invoke();
        }
    }

    public void OnPauseMenu(InputAction.CallbackContext context)
    {
        PauseMenuEvent?.Invoke();
        SetUIMap();
    }

    public void OnReturn(InputAction.CallbackContext context)
    {
        ReturnEvent?.Invoke();
        SetPlayerMap();
    }



    #region UICallbacks
    public void OnCancel(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }


    public void OnClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
