using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject aim;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (GamePaused)
            {
                Resume();
            }
            else
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
        Time.timeScale = 0.0f;
        GamePaused = true;
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




}
