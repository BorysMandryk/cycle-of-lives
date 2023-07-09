using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float MoveValue { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool FreezePressed { get; private set; }
    public bool PausePressed { get; private set; }

    private Controls _controls;

    private void OnEnable()
    {
        _controls = new Controls();

        _controls.Player.Move.performed += SetMove;
        _controls.Player.Move.canceled += SetMove;

        _controls.Player.Jump.performed += SetJump;
        _controls.Player.Jump.canceled += SetJump;

        _controls.Player.Freeze.performed += SetFreeze;
        _controls.Player.Freeze.canceled += SetFreeze;

        _controls.Player.PauseMenu.performed += SetPause;
        _controls.Player.PauseMenu.canceled += SetPause;

        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= SetMove;
        _controls.Player.Move.canceled -= SetMove;

        _controls.Player.Jump.performed -= SetJump;
        _controls.Player.Jump.canceled -= SetJump;

        _controls.Player.Freeze.performed += SetFreeze;
        _controls.Player.Freeze.canceled += SetFreeze;

        _controls.Player.PauseMenu.performed -= SetPause;
        _controls.Player.PauseMenu.canceled -= SetPause;

        _controls.Player.Disable();
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        MoveValue = context.ReadValue<float>();
    }

    private void SetJump(InputAction.CallbackContext context)
    {
        JumpPressed = context.ReadValueAsButton();
    }

    private void SetFreeze(InputAction.CallbackContext context)
    {
        FreezePressed = context.ReadValueAsButton();
    }

    private void SetPause(InputAction.CallbackContext context)
    {
        PausePressed = context.ReadValueAsButton();
    }
}
