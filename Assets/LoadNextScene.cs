using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string NextScene;
    public GameObject player;
    public GameObject allNextScenes;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLifeManagement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Scene actualScene = SceneManager.GetSceneAt(1);
            Scene followingScene = SceneManager.GetSceneByName(NextScene);
            SceneManager.UnloadSceneAsync(actualScene);
            SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);
            Destroy(allNextScenes.gameObject);
        }
    }

    IEnumerator LoadNxtScene()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
