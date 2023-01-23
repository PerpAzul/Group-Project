using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseScreenCanvas;
    public GameObject optionsScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
        }
        else
        {
            Stop();
        }
    }

    void Stop()
    {
        PauseScreenCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public void Play()
    {
        PauseScreenCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void openOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void closeOptions()
    {
        optionsScreen.SetActive(false);
    }
}

