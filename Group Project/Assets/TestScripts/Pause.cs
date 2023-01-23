using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private NewControls inputControls;
    
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;
    
    private bool isPaused;

    private void Awake()
    {
        inputControls = new NewControls();
    }

    private void OnEnable()
    {
        inputControls.Player.Pause.performed += doPause;
        inputControls.Player.Pause.Enable();
    }
    
    private void doPause(InputAction.CallbackContext obj)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            GamePaused.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            GameResumed.Invoke();
        }
    }
    
    private void OnDisable()
    {
        inputControls.Player.Pause.Disable();
    }
}
