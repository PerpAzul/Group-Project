using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    private NewControls inputControls;
    private InputAction menu;

    [SerializeField] private GameObject pauseUI;
    public bool isPaused;
    
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;
    
    public GameObject optionsScreen;


    // Start is called before the first frame update
    void Awake()
    {
        inputControls = new NewControls();
    }
    
    private void OnEnable()
    {
        inputControls.Player.Pause.performed += pause;

        inputControls.Player.Pause.Enable();

    }

    void pause(InputAction.CallbackContext context)
    {
        if (isPaused == false)
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            activateMenu();
        }
    }

    private void OnDisable()
    {
        inputControls.Player.Pause.Disable();
    }

    void activateMenu()
    {
        Time.timeScale = 0;
        GamePaused.Invoke();
        pauseUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void deactivateMenu()
    {
        optionsScreen.SetActive(false);
        isPaused = !isPaused;
        Time.timeScale = 1;
        GameResumed.Invoke();
        pauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void mainMenuButton()
    {
        Time.timeScale = 1;
        GameResumed.Invoke();
        SceneManager.LoadScene("GameMenu");
    }
    
    public void openOptions()
    {
        pauseUI.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void closeOptions()
    {
        pauseUI.SetActive(true);
        optionsScreen.SetActive(false);
    }
}
