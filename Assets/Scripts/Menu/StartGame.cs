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

    private void Start()
    {
        _camera = Camera.main;

        //Menu.SetActive(isOpened);
        if (GameManager.Instance.GameStarted)
        {
            DisableMenu();
            //OnCloseButtonClick();
            //InputReader.SetPlayerMap();

            InputReader.PauseMenuEvent += OnOpenButtonClick;
            InputReader.ReturnEvent += OnCloseButtonClick;
        }

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
        isOpened = true;        
        ExecuteTrigger(_camera.gameObject, "ShowMenu");
        ExecuteTrigger(Menu, "ShowMenu");
        _camera.GetComponent<PostProcessVolume>().profile = profile_on_menu;
    }

    public void OnCloseButtonClick()
    {
        isOpened = false;
        ExecuteTrigger(_camera.gameObject, "HideMenu");
        ExecuteTrigger(Menu, "HideMenu");
        _camera.GetComponent<PostProcessVolume>().profile = profile_on_game;
    }

    public void DisableMenu()
    {
        ExecuteTrigger(_camera.gameObject, "DisableMenu");
        ExecuteTrigger(Menu, "DisableMenu");
        _camera.GetComponent<PostProcessVolume>().profile = profile_on_game;
    }

    public void StartGameHandler()
    {
        if(!GameManager.Instance.GameStarted)
        {
            InputReader.PauseMenuEvent += OnOpenButtonClick;
            InputReader.ReturnEvent += OnCloseButtonClick;
        }

        GameManager.Instance.GameStarted = true;
        OnCloseButtonClick();

        InputReader.SetPlayerMap();
    }

    public void PauseGame()
    {
        OnOpenButtonClick();

        InputReader.SetUIMap();
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
