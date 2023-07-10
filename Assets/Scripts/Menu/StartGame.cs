using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class StartGame : MonoBehaviour
{
    public InputReader InputReader;

    public GameObject Menu;

    public bool isOpened = true;

    public PostProcessProfile profile_on_menu;
    public PostProcessProfile profile_on_game;

    private Camera _camera;

    private void Awake()
    {
        Menu.SetActive(isOpened);
        _camera = Camera.main;
        InputReader.PauseMenuEvent += OnOpenButtonClick;
        InputReader.ReturnEvent += OnCloseButtonClick;
    }

    private void OnDisable()
    {
        InputReader.PauseMenuEvent -= OnOpenButtonClick;
        InputReader.ReturnEvent -= OnCloseButtonClick;
    }

    private void ExecuteTrigger(GameObject target, string trigger)
    {
        if(target != null)
        {
            var animator = target.GetComponent<Animator>();
            if(animator != null )
            {
                animator.SetTrigger(trigger);
            }
        }
    }

    public void OnOpenButtonClick()
    {
        ExecuteTrigger(_camera.gameObject, "ShowMenu");
        ExecuteTrigger(Menu, "ShowMenu");
        _camera.GetComponent<PostProcessVolume>().profile = profile_on_menu;
    }

    public void OnCloseButtonClick()
    {
        ExecuteTrigger(_camera.gameObject, "HideMenu");
        ExecuteTrigger(Menu, "HideMenu");
        _camera.GetComponent<PostProcessVolume>().profile = profile_on_game;
    }

}
