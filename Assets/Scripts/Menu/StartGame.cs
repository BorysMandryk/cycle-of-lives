using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Menu;

    public bool isOpened = true;

    public InputAction pause;

    private void ExecuteTrigger(GameObject target, string trigger)
    {
        if(Camera != null)
        {
            var animator = target.GetComponent<Animator>();
            if(animator != null )
            {
                animator.SetTrigger(trigger);
            }
        }
    }

    private void Update()
    {
        if (isOpened)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnCloseButtonClick();
            }

            isOpened = false;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnOpenButtonClick();
            }

            isOpened = true;
        }

    }
    public void OnOpenButtonClick()
    {
        ExecuteTrigger(Camera, "ShowMenu");
        ExecuteTrigger(Menu, "ShowMenu");
    }

    public void OnCloseButtonClick()
    {
        ExecuteTrigger(Camera, "HideMenu");
        ExecuteTrigger(Menu, "HideMenu");
    }

}
