using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool SettingsMenu = false;

    public GameObject pauseMenuUI;
    public GameObject aim;
    public GameObject SettingsCanvas;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused && SettingsMenu)
            {
                Pause(); 
            }
            else if (GamePaused)
            {
                Resume();
            }
            else if(!GamePaused)
            {
                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (GamePaused)
            {
                Forfait();
            }
        }
        
    }
   public void Resume()
    {
        Cursor.visible = false;
        aim.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;
    }
    public void Pause()
    {
        Cursor.visible = true;
        aim.SetActive(false);
        pauseMenuUI.SetActive(true);
        SettingsCanvas.SetActive(false);
        Time.timeScale = 0.0f;
        GamePaused = true;
        SettingsMenu = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;

    }
    public void Forfait()
    {
        SceneManager.LoadScene("Init Scene 1");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;

    }

    public void Settings(GameObject component)
    {
        SettingsMenu = true;
        
        component.SetActive(true);
        Time.timeScale = 0.0f;
    }




}
