using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string _scene;
    public bool isAdditive;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Init Game", LoadSceneMode.Additive);
        LoadWantedSceneAdditive(_scene);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadWantedSceneAdditive(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void LoadWantedScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
