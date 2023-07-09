using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Controls Controls { get; private set; }

    private void Awake()
    {
        Controls = new Controls();
        ToggleActionMap(Controls.Player);
    }

    public void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
        {
            return;
        }

        Controls.Disable();
        actionMap.Enable();
    }
}
