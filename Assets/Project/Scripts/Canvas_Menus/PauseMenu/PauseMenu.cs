using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool SettingsMenu = false;

    public GameObject pauseMenuUI;
    public GameObject aim;
    public GameObject SettingsCanvas;
    public Button forfaitButton;
    public Button mainMenuButton;


    [SerializeField] GameObject playerController;

    private NewAudioManager audioManager;
    private string actualScene;
    private bool once = true;



    private void Start()
    {
        playerController = FindObjectOfType<Player_Controller>().gameObject;

        audioManager = FindObjectOfType<NewAudioManager>();

        forfaitButton.interactable = false;

        actualScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenuButton.animator.Play("Normal");

            if (once)
            {
                StartCoroutine(CheckActualScene());
                once = !once;
            }
            
            if (GamePaused && SettingsMenu)
            {
                Pause();
            }
            else if (GamePaused)
            {
                Resume();
            }
            else if (!GamePaused)
            {
                Pause();
                audioManager.FadeOutMaster("MasterVolume");
            }
        }

        if (actualScene != "Lobby")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (GamePaused)
                {
                    Forfait();
                }
            }
            forfaitButton.interactable = true;
        }

    }

    public void Resume()
    {
        audioManager.FadeInMaster("MasterVolume");
        Cursor.visible = false;
        aim.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;
        playerController.GetComponent<Player_Controller>()._canMove = true;



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
        playerController.GetComponent<Player_Controller>()._canMove = false;

    }
    public void LoadMenu()
    {
        //poner el boton de Main Menu a Normal
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;

    }
    public void Forfait()
    {

        audioManager.SetMasterVolume(0.0f);
        SceneManager.LoadScene("ScoreReward");
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

    IEnumerator CheckActualScene()
    {
        do
        {
            Debug.Log("Corrutine");
            actualScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
            yield return new WaitForSecondsRealtime(0.25f);

        } while (true);

    }




}
