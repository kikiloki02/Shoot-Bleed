using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string _scene;
    public bool isAdditive;
    public bool unloadPreviousScene;
    public bool loadOnCollision;
    public Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Init Game", LoadSceneMode.Additive);
        //LoadWantedSceneAdditive(_scene);
        if (!loadOnCollision && _scene != null)
        {
            if (isAdditive)
            {
                LoadWantedSceneAdditive(_scene);
            }
            else
            {
                LoadWantedScene(_scene);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadWantedSceneAdditive(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

        if (unloadPreviousScene)
        {
            string actualScene = SceneManager.GetSceneAt(1).name;

            SceneManager.UnloadSceneAsync(actualScene);
        }
    }

    public void LoadWantedScene(string scene)
    {
        SceneManager.LoadScene(scene);

        if (unloadPreviousScene)
        {
            string actualScene = SceneManager.GetSceneAt(1).name;

            SceneManager.UnloadSceneAsync(actualScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_scene != null) { return; }
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAdditive)
            {
                LoadWantedSceneAdditive(_scene);
            }
            else
            {
                LoadWantedScene(_scene);
            }
        }
    }



}
