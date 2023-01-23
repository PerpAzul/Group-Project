using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string map;
    public GameObject optionsScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(map);
    }

    public void openOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void closeOptions()
    {
        optionsScreen.SetActive(false);
    }
    
    public void quitGame()
    {
        Application.Quit();
    }
}
